using Core.Requests.AdminRequests.FriendCategoryRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class FriendCategoriesController : Controller
{
    private readonly IMediator _mediator;
    public FriendCategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _mediator.Send(new GetAllFriendCategoriesQuery()));
    }
} 