using Contracts.Requests.FooterFriendsSectionRequests;
using Contracts.Requests.FooterFriendsSectionRequests.GetFriendsList;
using MediatR;

namespace Core.Requests.FooterFriendsSectionRequests.GetFriendsList
{
    public class GetFriendsListQuery : IRequest<GetFriendsListResponse>
    {
    }
}