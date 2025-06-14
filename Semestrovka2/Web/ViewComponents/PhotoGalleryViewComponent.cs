using Core.Abstractions;
using Core.Requests.PhotoRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.ViewComponents
{
    public class PhotoGalleryViewComponent(IMediator mediator, IUserContext userContext) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Guid userId, bool? sortByDate = null, bool? sortByAmount = null)
        {
            var albums = await mediator.Send(new GetAlbumsQuery
            {
                UserId = userId,
                SortByDate = sortByDate,
                SortByAmount = sortByAmount
            });
            return View("PhotoGallery", albums);
        }
    }
}