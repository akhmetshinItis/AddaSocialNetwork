using Contracts.Requests.AdminRequests.FriendCategoriesRequests;
using MediatR;

namespace Core.Requests.AdminRequests.FriendCategoryRequests
{
    public class GetAllFriendCategoriesQuery : IRequest<GetAllFriendCategoriesResponse>
    {
    }
}