using Core.Requests.AdminRequests.PostRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

[Area("Admin")]
public class PostsController : Controller
{
    private readonly IMediator _mediator;
    
    public PostsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _mediator.Send(new GetAllPostsQuery()));
    }
} 