using Core.Requests.AdminRequests.UserRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

public class UsersController : AdminBaseController
{
    private readonly IMediator _mediator;
    
    public UsersController(IMediator mediator) : base(mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _mediator.Send(new GetAllUsersQuery()));
    }
} 