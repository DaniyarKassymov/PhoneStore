using Lesson.Context;
using Lesson.Mappers;
using Lesson.Models;
using Lesson.Options;
using Lesson.Services;
using Lesson.ViewModels;
using Lesson.ViewModels.BrandVms;
using Lesson.ViewModels.PhoneVms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Lesson.Controllers;

public class PhoneController : Controller
{
    private readonly MobileContext _context;
    private readonly IOptionsMonitor<CurrencyInfo> _options;
    private readonly IFileService _fileService;

    public PhoneController(
        MobileContext context, 
        IOptionsMonitor<CurrencyInfo> options, 
        IFileService fileService)
    {
        _context = context;
        _options = options;
        _fileService = fileService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int page = 1)
    {
        IQueryable<Phone> phones = _context.Phones
            .Include(p => p.Brand);
        var count = await phones.CountAsync();

        int pageSize = 10;
        phones = phones
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
        
        var pageViewModel = new PageVm(count, page, pageSize);
        var vms = await phones.AsNoTracking().Select(p => p.ToVm()).ToListAsync();
        
        return View(new PhonesListVm{Phones = vms, PageVm = pageViewModel});
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public IActionResult Add()
    {
        var vm = new PhoneFormVm();
        GetBrands(vm);
        
        vm.Brands!.FirstOrDefault(b => b.Value == "1")!.Selected = true;
        return View(vm);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Add(PhoneFormVm? vm)
    {
        if (vm != null && ModelState.IsValid)
        {
            var path= await _fileService.UploadFileAsync(vm.Photo, vm.Title);

            _context.Phones.Add(vm.ToModel(path));
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index");
        }
        
        GetBrands(vm);
        return View("Add", vm);
    }

    private void GetBrands(PhoneFormVm? vm)
    {
        var brands = _context.Brands.Select(brand => brand.ToViewModel()).ToList();
        vm!.Brands = new SelectList(
            brands,
            nameof(BrandVm.Id),
            nameof(BrandVm.Name),
            brands[0]);

        vm.Brands.FirstOrDefault(b => b.Value == "1")!.Selected = true;
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public IActionResult Edit(long id)
    {
        var phone = _context.Phones.Find(id);
        if (phone != null)
        {
            var editPhoneVm = PhoneFormVm.FromModel(phone);
            var brands = _context.Brands
                .Select(brand => brand.ToViewModel())
                .ToList();
            editPhoneVm.Brands = new SelectList(brands, "Id", "Name");
            return View(editPhoneVm);
        }

        return View();
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public IActionResult Edit(PhoneFormVm formVm)
    {
        var phone = formVm.ToModel("");
        
        _context.Phones.Update(phone);
        _context.SaveChanges();
        
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Details(long phoneId)
    {
        var phone = _context.Phones
            .Include(p => p.Brand)
            .FirstOrDefault(phone => phone.Id == phoneId);
        
        if (phone == null)
            return NotFound();
        
        var phoneDetailsVm = PhoneDetailsVm.FromModel(phone);
        phoneDetailsVm.CurrencyInfo = _options.CurrentValue;
        return View(phoneDetailsVm);
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public IActionResult Delete(long phoneId)
    {
        var phone = _context.Phones.Find(phoneId);
        _context.Phones.Remove(phone!);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}