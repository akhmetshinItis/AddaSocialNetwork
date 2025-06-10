namespace Contracts.Requests.HomePageRequests
{
    public class PostResponseItem
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserPhoto { get; set; }
        public string? Content { get; set; }
        public string? Photo { get; set; }
        public int Likes { get; set; } = new();
        public bool IsLiked { get; set; }
        public int CommentsCount { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Time { get; set; }
    }
}