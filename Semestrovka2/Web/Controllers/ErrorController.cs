using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class ErrorController : Controller
{
    [Route("Error/NotFound")]
    public IActionResult NotFound()
    {
        return View();
    }

    [Route("Error/Forbidden")]
    public IActionResult Forbidden()
    {
        return View();
    }

    [Route("Error/ServerError")]
    public IActionResult ServerError()
    {
        return View();
    }
} 