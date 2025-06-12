using Contracts.Requests.AdminRequests.FriendRequests;
using MediatR;

namespace Core.Requests.AdminRequests.FriendRequests
{
    public class GetAllFriendsQuery : IRequest<GetAllFriendsResponse>
    {
    }
} 