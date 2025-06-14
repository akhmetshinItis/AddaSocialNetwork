using Core.Requests.FooterFriendsSectionRequests.GetFriendsList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.ViewComponents
{
    public class MoFriendListViewComponent(IMediator mediator) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var friendsList = await mediator.Send(new GetFriendsListQuery());
            return View("MoFriendsList" ,friendsList);
        }
    }
}