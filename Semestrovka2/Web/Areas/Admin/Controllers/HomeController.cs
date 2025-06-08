using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

[Area("admin")]
public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}