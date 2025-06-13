using Core.Abstractions;
using Core.Requests.PhotoRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Authorize]
    public class PhotosController(IMediator mediator, IUserContext userContext) : Controller
    {
        public IActionResult Index()
        {
            return View(userContext.GetUserId());
        }
        
        [HttpGet("photos/{userId:guid}")]
        public IActionResult ByUser([FromRoute] Guid userId, bool? sortByDate = null, bool? sortByAmount = null)
        {
            ViewBag.SortByDate = sortByDate;
            ViewBag.SortByAmount = sortByAmount;
            return View("~/Views/Photos/Index.cshtml", userId);        }

        [HttpGet("allAlbums/{userId:guid}")]
        public async Task<IActionResult> GetAllAlbumsAsync([FromRoute] Guid? userId, bool? sortByDate = null, bool? sortByAmount = null)
            => Ok(await mediator.Send(new GetAlbumsQuery
            {
                UserId = userId,
                SortByDate = sortByDate,
                SortByAmount = sortByAmount
            }));

        [HttpPost("createAlbum")]
        public async Task<IActionResult> CreateAlbum([FromForm] CreateAlbumCommand command)
        {
            command.UserId = userContext.GetUserId();
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}