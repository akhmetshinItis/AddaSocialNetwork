using Core.Requests.ProfileRequests;
using Core.Requests.ProfileRequests.GetProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ProfileController(IMediator mediator) : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public async Task<IActionResult> Index()
            =>View(await mediator.Send(new GetProfileQuery()));
        
        [HttpGet("profile/{id:guid}")]
        public async Task<IActionResult> IndexById([FromRoute] Guid id)
            =>View("Index", await mediator.Send(new GetProfileQuery
            {
                IsCurrentUserProfile = false,
                UserId = id
            }));
        
        
        [HttpGet("profilePhotos")]
        public async Task<IActionResult> ProfilePhotos()
            =>Ok(await mediator.Send(new GetProfileQuery()));
        
        [HttpGet("profilePhotos/{id:guid}")]
        public async Task<IActionResult> ProfilePhotos([FromRoute] Guid id)
            => Ok(await mediator.Send(new GetProfileQuery
            {
                IsCurrentUserProfile = false,
                UserId = id
            }));
    }
}