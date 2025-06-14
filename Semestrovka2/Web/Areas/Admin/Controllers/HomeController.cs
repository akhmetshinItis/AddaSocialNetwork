using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

[Area("admin")]
[Authorize(Roles = "Admin,Owner")]
public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}