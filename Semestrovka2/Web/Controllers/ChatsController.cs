using Core.Requests.ChatRequests.GetChatByFriendId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ChatsController(IMediator mediator) : Controller
    {
        [HttpGet("api/chats/getByFriendId/{friendId}")]
        public async Task<IActionResult> GetByFriendId(Guid friendId)
        {
            if(friendId == Guid.Empty)
                throw new ArgumentNullException(nameof(friendId));
            
            return Ok(await mediator.Send(new GetChatByFriendIdQuery(friendId)));
        }
    }
}