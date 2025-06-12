using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

public class CommentsController : Controller
{
    public CommentsController(IMediator mediator)
    {
    }

    public IActionResult Index()
    {
        return View();
    }
} 