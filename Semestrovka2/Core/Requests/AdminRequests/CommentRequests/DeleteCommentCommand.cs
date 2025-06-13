using MediatR;

namespace Core.Requests.AdminRequests.CommentRequests
{
    public class DeleteCommentCommand : IRequest
    {
        public Guid CommentId { get; set; }
    }
} 