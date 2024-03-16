using Lesson.Context;
using Lesson.Models;
using Lesson.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lesson.Controllers;

public class OrderController : Controller
{
    private readonly MobileContext _context;

    public OrderController(MobileContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Index()
    {
        return View(
            _context
                .Orders
                .Include(o => o.Phone)
                .ToList());
    }

    [HttpGet]
    [Authorize(Roles = "user")]
    public IActionResult Checkout(long phoneId)
    {
        var phone = _context.Phones.
            Include(p => p.Brand)
            .FirstOrDefault(p => p.Id == phoneId)!;
        
        var vm = new CheckoutOrderVm
        {
            PhoneId = phoneId,
            PhoneName = $"{phone.Brand?.Name} {phone.Title}"
        };
        return View(vm);
    }

    [HttpPost]
    [Authorize(Roles = "user")]
    public IActionResult Checkout(CheckoutOrderVm vm)
    {
        var order = new Order
        {
            Address = vm.Address,
            Name = vm.Name,
            ContactPhone = vm.ContactPhone,
            PhoneId = vm.PhoneId
        };
        _context.Orders.Add(order);
        _context.SaveChanges();
        
        return RedirectToAction("Index");
    }
}