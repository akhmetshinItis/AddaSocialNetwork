using MediatR;

namespace Core.Requests.FriendsRequests
{
    public class AddFriendCategoryRequest : IRequest<Unit>, IRequest
    {
        public Guid FriendId { get; set; }
        public string Category { get; set; }
    }
} 