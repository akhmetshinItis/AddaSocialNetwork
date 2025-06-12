using Contracts.Requests.AdminRequests.CommentRequests;
using MediatR;

namespace Core.Requests.AdminRequests.CommentRequests
{
    public class GetAllCommentsQuery : IRequest<GetAllCommentsResponse>
    {
    }
} 