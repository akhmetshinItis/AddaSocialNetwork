using Core.Requests.AdminRequests.HobbyRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

[Area("Admin")]
public class HobbiesController : Controller
{
    private readonly IMediator _mediator;
    
    public HobbiesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _mediator.Send(new GetAllHobbiesQuery()));
    }
} 