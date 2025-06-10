using Core.Abstractions;
using Core.Requests.PhotoRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class PhotosController(IMediator mediator, IUserContext userContext) : Controller
    {
        public IActionResult Index()
        {
            return View(userContext.GetUserId());
        }
        
        [HttpGet("photos/{userId:guid}")]
        public IActionResult ByUser([FromRoute] Guid userId)
        {
            return View("Index", userId);
        }

        [HttpGet("allAlbums/{userId:guid}")]
        public async Task<IActionResult> GetAllAlbumsAsync([FromRoute] Guid? userId)
            => Ok(await mediator.Send(new GetAlbumsQuery
            {
                UserId = userId,
            }));
    }
}