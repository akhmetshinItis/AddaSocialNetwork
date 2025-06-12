using Core.Requests.AdminRequests.FriendRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

[Area("Admin")]
public class FriendsController : Controller
{
    private readonly IMediator _mediator;
    
    public FriendsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _mediator.Send(new GetAllFriendsQuery()));
    }
} 