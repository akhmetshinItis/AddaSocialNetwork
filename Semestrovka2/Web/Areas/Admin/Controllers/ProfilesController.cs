using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

public class ProfilesController : Controller
{
    public ProfilesController(IMediator mediator)
    {
    }

    public IActionResult Index()
    {
        return View();
    }
} 