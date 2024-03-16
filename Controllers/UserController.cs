using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Lesson.Context;
using Lesson.Enums;
using Lesson.Mappers;
using Lesson.Models;
using Lesson.ViewModels;
using Lesson.ViewModels.UserAuthVms;
using Lesson.ViewModels.UserVms;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lesson.Controllers;

public class UserController : Controller
{
    private readonly MobileContext _db;

    public UserController(MobileContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index(
        string? name,
        long? brandId,
        int page = 1,
        SortState sortOrder = SortState.NameAsc)
    {
       
        IQueryable<User> users = _db.Users.Include(user => user.Brand);
        var count = await users.CountAsync();

        int pageSize = 3;
        users = users
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
        
        users = FilterUsers(name, brandId, users);
        users = SortUsers(sortOrder, users);

        var pageViewModel = new PageVm(count, page, pageSize);
        
        var brands = await _db.Brands.ToListAsync();
        brands.Add(new Brand {Id = 0, Name = "Все"});
        
        var usersListViewModel = new UsersListVm
        {
            Users = await users.AsNoTracking().Select(u => u.ToVm()).ToListAsync(),
            Brands = new SelectList(brands, "Id", "Name"),
            Name = name,
            PageVm = pageViewModel
        };
        return View(usersListViewModel);
    }

    private static IQueryable<User> FilterUsers(string? name, long? brandId, IQueryable<User> users)
    {
        if (!string.IsNullOrEmpty(name))
            users = users.Where(user => EF.Functions.Like(user.Name, $"{name}%"));

        if (brandId.HasValue && brandId.Value != 0)
            users = users.Where(user => user.Brand!.Id == brandId);
        return users;
    }

    private IQueryable<User> SortUsers(SortState sortOrder, IQueryable<User> users)
    {
        ViewBag.NameSort = sortOrder == SortState.NameAsc
            ? SortState.NameDesc
            : SortState.NameAsc;

        ViewBag.AgeSort = sortOrder == SortState.AgeAsc
            ? SortState.AgeDesc
            : SortState.AgeAsc;

        ViewBag.BrandSort = sortOrder == SortState.BrandAsc
            ? SortState.BrandDesc
            : SortState.BrandAsc;

        users = sortOrder switch
        {
            SortState.NameAsc => users.OrderBy(user => user.Name),
            SortState.NameDesc => users.OrderByDescending(user => user.Name),
            SortState.AgeAsc => users.OrderBy(user => user.Age),
            SortState.AgeDesc => users.OrderByDescending(user => user.Age),
            SortState.BrandAsc => users.OrderBy(user => user.Brand!.Name),
            SortState.BrandDesc => users.OrderByDescending(user => user.Brand!.Name),
            _ => throw new ArgumentOutOfRangeException(nameof(sortOrder), sortOrder, null)
        };
        return users;
    }
    
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterVm registerViewModel)
    {
        if (ModelState.IsValid)
        {
            var userAuth = await _db.Users.FirstOrDefaultAsync(u =>
                u.Email == registerViewModel.Email &&
                u.Password == registerViewModel.Password);
            if (userAuth is null)
            {
                var role = (await _db.UserAuthRoles.FirstOrDefaultAsync(r => r.Name.Equals("user")))!;
                var user = await _db.Users.AddAsync(new User()
                {
                    Email = registerViewModel.Email,
                    Password = registerViewModel.Password,
                    UserAuthRole = role
                });
                await _db.SaveChangesAsync();
                await Authenticate(user.Entity);
                return RedirectToAction("Index", "Phone");
            }
            ModelState.AddModelError(nameof(registerViewModel.Email), "Email занят");
        }

        return View(registerViewModel);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVm loginVm)
    {
        if (ModelState.IsValid)
        {
            var userLogin = await _db.Users
                .Include(user => user.UserAuthRole)
                .FirstOrDefaultAsync(user =>
                user.Email == loginVm.Email &&
                user.Password == loginVm.Password);
            if (userLogin is not null)
            {
                await Authenticate(userLogin);
                return RedirectToAction("Index", "Phone");
            }
        }
        
        ModelState.AddModelError("Incorrect", "Некорректные логин и/или пароль");
        return View(loginVm);
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    [NonAction]
    private async Task Authenticate(User user)   
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.UserAuthRole.Name)
        };
        
        var id = new ClaimsIdentity(
            claims,
            "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(id),
            new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(1)
            });
    }
}