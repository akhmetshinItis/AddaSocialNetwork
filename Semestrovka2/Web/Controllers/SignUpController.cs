using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class SignUpController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public SignUpController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}