using Core.Requests.AdminRequests.ChatRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ChatsController : Controller
{
    private readonly IMediator _mediator;
    
    public ChatsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _mediator.Send(new GetAllChatsQuery()));
    }
} 