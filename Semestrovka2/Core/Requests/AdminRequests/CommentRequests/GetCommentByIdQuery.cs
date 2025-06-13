using Contracts.Requests.AdminRequests.CommentRequests;
using MediatR;

namespace Core.Requests.AdminRequests.CommentRequests
{
    public class GetCommentByIdQuery : IRequest<GetAllCommentsResponseCommentItem>
    {
        public Guid Id { get; set; }
    }
} 