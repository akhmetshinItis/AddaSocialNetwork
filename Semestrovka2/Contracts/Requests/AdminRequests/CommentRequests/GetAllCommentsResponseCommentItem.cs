namespace Contracts.Requests.AdminRequests.CommentRequests
{
    public class GetAllCommentsResponseCommentItem
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = default!;
        public Guid UserId { get; set; }
        public string UserName { get; set; } = default!;
        public Guid PostId { get; set; }
        public string PostContent { get; set; } = default!;
        public DateTime? CreatedDate { get; set; }
    }
} 