using Contracts.Requests.AdminRequests.PostRequests;
using MediatR;

namespace Core.Requests.AdminRequests.PostRequests
{
    public class GetAllPostsQuery : IRequest<GetAllPostsResponse>
    {
    }
} 