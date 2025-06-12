using Core.Requests.AdminRequests.PhotoRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

[Area("Admin")]
public class PhotosController : Controller
{
    private readonly IMediator _mediator;
    
    public PhotosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _mediator.Send(new GetAllPhotosQuery()));
    }
} 