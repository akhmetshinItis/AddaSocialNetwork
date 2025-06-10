using Core.Requests.FooterFriendsSectionRequests.GetFriendsList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.ViewComponents
{
    public class FriendTableViewComponent(IMediator mediator) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Guid userId)
        {
            var result = await mediator.Send(new GetFriendsListQuery
            {
                UserId = userId
            });

            return View("FriendTable", result);
        }
    }
}