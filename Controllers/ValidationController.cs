using Lesson.Context;
using Microsoft.AspNetCore.Mvc;

namespace Lesson.Controllers;

public class ValidationController : Controller
{
    private readonly MobileContext _context;

    public ValidationController(MobileContext context)
    {
        _context = context;
    }

    public bool CheckName(string name)
    {
      return !_context.Brands.Any(b => b.Name == name);
    }
}