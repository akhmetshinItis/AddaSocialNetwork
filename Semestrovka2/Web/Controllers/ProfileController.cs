using Core.Abstractions;
using Core.Requests.ProfileRequests.GetProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ProfileController(IMediator mediator, IUserContext userContext) : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public async Task<IActionResult> Index()
            =>View(await mediator.Send(new GetProfileQuery{IsCurrentUserProfile = true}));
        
        [HttpGet("profile/{id:guid}")]
        public async Task<IActionResult> IndexById([FromRoute] Guid id)
            =>View("Index", await mediator.Send(new GetProfileQuery
            {
                IsCurrentUserProfile = userContext.GetUserId() == id,
                UserId = id
            }));
        
        
        [HttpGet("profilePhotos")]
        public async Task<IActionResult> ProfilePhotos()
            =>Ok(await mediator.Send(new GetProfileQuery{IsCurrentUserProfile = true}));
        
        [HttpGet("profilePhotos/{id:guid}")]
        public async Task<IActionResult> ProfilePhotos([FromRoute] Guid id)
            => Ok(await mediator.Send(new GetProfileQuery
            {
                IsCurrentUserProfile = userContext.GetUserId() == id,
                UserId = id
            }));
    }
}