using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

[Area("Admin")]
public abstract class AdminBaseController : Controller
{
    protected readonly IMediator Mediator;

    protected AdminBaseController(IMediator mediator)
    {
        Mediator = mediator;
    }

    protected IActionResult AdminView()
    {
        return View();
    }
} 