using Core.Requests.ProfileRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ProfileController(IMediator mediator) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
            =>View(await mediator.Send(new GetProfileCommand()));
        
        [HttpGet("Index/{id:guid}")]
        public async Task<IActionResult> Index([FromRoute] Guid id)
            =>View(await mediator.Send(new GetProfileCommand
            {
                IsCurrentUserProfile = false,
                UserId = id
            }));
    }
}