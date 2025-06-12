using Core.Abstractions;
using Core.Requests.ProfileRequests.GetProfile;
using Core.Requests.ProfileRequests.UpdateProfile;
using Core.Requests.ProfileRequests.UpdateProfileImage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Authorize]
    public class ProfileController(IMediator mediator, IUserContext userContext) : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public async Task<IActionResult> Index()
            =>View(await mediator.Send(new GetProfileQuery{IsCurrentUserProfile = true}));

        [HttpGet("profile/{id:guid}")]
        public async Task<IActionResult> IndexById([FromRoute] Guid id)
        {
            var response = await mediator.Send(new GetProfileQuery
            {
                IsCurrentUserProfile = userContext.GetUserId() == id,
                UserId = id
            });
            
            return View("Index", response);
        }
        
        
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

        [HttpPost("api/updateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }

        [HttpPost("api/updateProfileImage")]
        public async Task<IActionResult> UpdateProfileImage([FromForm] UpdateProfileImageRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }
    }
}