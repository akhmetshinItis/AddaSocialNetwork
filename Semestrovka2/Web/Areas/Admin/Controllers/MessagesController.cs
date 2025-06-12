using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

public class MessagesController : Controller
{
    public MessagesController(IMediator mediator)
    {
    }

    public IActionResult Index()
    {
        return View();
    }
} 