using MediatR;

namespace Core.Requests.AdminRequests.CommentRequests
{
    public class CreateCommentCommand : IRequest
    {
        public string? Content { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
    }
} 