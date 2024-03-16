using Lesson.Context;
using Lesson.ViewModels;
using Lesson.ViewModels.BrandVms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lesson.Controllers;

public class BrandController : Controller
{
    private readonly MobileContext _context;

    public BrandController(MobileContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var brands = _context.Brands.ToList();
        var brandVms = brands.Select(brand => brand.ToViewModel()).ToList();
        
        return View(brandVms);
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public IActionResult Add(AddBrandVm? vm)
    {
        if (vm != null && ModelState.IsValid)
        {
            _context.Brands.Add(vm.ToModel());
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(vm);
    }
}