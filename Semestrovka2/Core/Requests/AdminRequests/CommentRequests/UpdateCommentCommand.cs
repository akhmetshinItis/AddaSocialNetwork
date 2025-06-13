using MediatR;

namespace Core.Requests.AdminRequests.CommentRequests
{
    public class UpdateCommentCommand : IRequest
    {
        public Guid CommentId { get; set; }
        public string? Content { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
    }
} 