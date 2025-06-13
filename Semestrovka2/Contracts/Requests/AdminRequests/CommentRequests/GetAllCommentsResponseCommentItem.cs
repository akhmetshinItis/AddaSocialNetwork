namespace Contracts.Requests.AdminRequests.CommentRequests
{
    public class GetAllCommentsResponseCommentItem
    {
        public Guid Id { get; set; }
        public string? Content { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = default!;
        public DateTime? CreatedDate { get; set; }
    }
} 