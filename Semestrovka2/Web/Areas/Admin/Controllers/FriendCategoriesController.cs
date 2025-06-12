using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

public class FriendCategoriesController : Controller
{
    public FriendCategoriesController(IMediator mediator)
    {
    }

    public IActionResult Index()
    {
        return View();
    }
} 