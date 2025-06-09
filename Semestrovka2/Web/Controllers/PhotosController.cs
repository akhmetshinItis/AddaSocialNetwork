using Core.Requests.PhotoRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class PhotosController(IMediator mediator) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await mediator.Send(new GetAlbumsQuery()));
        }
        
        [HttpGet("photos/{userId:guid}")]
        public async Task<IActionResult> ByUser([FromRoute] Guid userId)
        {
            var result = await mediator.Send(new GetAlbumsQuery
            {
                UserId = userId
            });

            return View("Index", result);
        }

        [HttpGet("allAlbums/{userId:guid}")]
        public async Task<IActionResult> GetAllAlbumsAsync([FromRoute] Guid? userId)
            => Ok(await mediator.Send(new GetAlbumsQuery
            {
                UserId = userId,
            }));
    }
}