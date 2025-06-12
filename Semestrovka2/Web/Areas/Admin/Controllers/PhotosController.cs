using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

public class PhotosController : Controller
{
    public PhotosController(IMediator mediator)
    {
    }

    public IActionResult Index()
    {
        return View();
    }
} 