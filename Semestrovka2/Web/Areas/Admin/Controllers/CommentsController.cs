using Core.Requests.AdminRequests.CommentRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

[Area("Admin")]
public class CommentsController : Controller
{
    private readonly IMediator _mediator;
    
    public CommentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _mediator.Send(new GetAllCommentsQuery()));
    }
} 