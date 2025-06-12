using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

public class FriendsController : Controller
{
    public FriendsController(IMediator mediator)
    {
    }

    public IActionResult Index()
    {
        return View();
    }
} 