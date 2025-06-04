using Contracts.Requests.ChatRequests.GetChatByFriendId;
using MediatR;

namespace Core.Requests.ChatRequests.GetChatByFriendId
{
    public class GetChatByFriendIdQuery(Guid friendId) : IRequest<GetChatByFriendIdResponse>
    {
        public Guid FriendId { get; set; } = friendId;
    }
}