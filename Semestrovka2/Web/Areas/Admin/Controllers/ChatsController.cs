using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

public class ChatsController : Controller
{
    public ChatsController(IMediator mediator)
    {
    }

    public IActionResult Index()
    {
        return View();
    }
} 