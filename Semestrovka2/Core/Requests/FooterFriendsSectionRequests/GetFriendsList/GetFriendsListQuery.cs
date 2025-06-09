using Contracts.Requests.FriendsRequests.GetFriendsList;
using MediatR;

namespace Core.Requests.FooterFriendsSectionRequests.GetFriendsList
{
    public class GetFriendsListQuery : IRequest<GetFriendsListResponse>
    {
        public Guid? UserId { get; set; }
    }
}