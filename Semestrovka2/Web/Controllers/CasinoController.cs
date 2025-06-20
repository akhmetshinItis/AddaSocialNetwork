using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class CasinoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}