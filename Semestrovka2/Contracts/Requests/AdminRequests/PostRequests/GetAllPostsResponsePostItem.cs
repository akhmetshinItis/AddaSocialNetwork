namespace Contracts.Requests.AdminRequests.PostRequests
{
    public class GetAllPostsResponsePostItem
    {
        public Guid Id { get; set; }
        public string? Content { get; set; }
        public string? Photo { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = default!;
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
} 