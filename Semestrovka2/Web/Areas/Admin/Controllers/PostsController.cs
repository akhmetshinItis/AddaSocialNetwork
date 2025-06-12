using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

public class PostsController : Controller
{
    public PostsController(IMediator mediator)
    {
    }

    public IActionResult Index()
    {
        return View();
    }
} 