using System.Diagnostics;
using Core.Requests.FooterFriendsSectionRequests.AddFriend;
using Core.Requests.FooterFriendsSectionRequests.GetFriendsList;
using Core.Requests.FooterFriendsSectionRequests.SearchFriends;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class FriendsController(IMediator mediator) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var friends = await mediator.Send(new GetFriendsListQuery());
            return View(friends);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        [HttpGet("api/searchFriends")]
        public async Task<IActionResult> SearchFriends(string? query)
        {
            var friendsList = await mediator.Send(new SearchFriendsQuery(query));
            return PartialView("_FriendSearchResults", friendsList);
        }

        [HttpPost("api/addFriend")]
        public async Task<IActionResult> AddFriend(string friendId)
        {
            Guid id;
            if (Guid.TryParse(friendId, out id))
            {
                var response = await mediator.Send(new AddFriendCommand(id));
                return Ok(response.RowsAffected);
            }
            return BadRequest();
        }
    }
}