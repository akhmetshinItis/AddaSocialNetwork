using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public ProfileController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}