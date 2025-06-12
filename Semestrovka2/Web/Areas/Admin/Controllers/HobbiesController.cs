using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

public class HobbiesController : Controller
{
    public HobbiesController(IMediator mediator)
    {
    }

    public IActionResult Index()
    {
        return View();
    }
} 