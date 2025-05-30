using Contracts.Requests.FriendsRequests.AddFriend;
using MediatR;

namespace Core.Requests.FooterFriendsSectionRequests.AddFriend
{
    public class AddFriendCommand : IRequest<AddFriendResponse>
    {
        public Guid FriendId { get; set; }

        public AddFriendCommand(Guid friendId)
        {
            FriendId = friendId;
        }
    }
}