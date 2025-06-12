using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

public class AlbumsController : Controller
{
    public AlbumsController(IMediator mediator)
    {
    }

    public IActionResult Index()
    {
        return View();
    }
} 