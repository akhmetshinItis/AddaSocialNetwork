using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Owner")]
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