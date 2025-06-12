using Core.Requests.AdminRequests.MessageRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

[Area("Admin")]
public class MessagesController : Controller
{
    private readonly IMediator _mediator;
    
    public MessagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _mediator.Send(new GetAllMessagesQuery()));
    }
} 