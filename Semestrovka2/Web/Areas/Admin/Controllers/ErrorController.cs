using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ErrorController : Controller
{
    public IActionResult NotFound()
    {
        return View();
    }

    public IActionResult Forbidden()
    {
        return View();
    }
} 