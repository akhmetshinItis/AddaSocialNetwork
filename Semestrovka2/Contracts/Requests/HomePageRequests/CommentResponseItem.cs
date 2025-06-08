namespace Contracts.Requests.HomePageRequests
{
    public class CommentResponseItem
    {
        public Guid UserId { get; set; }
        public string UserPhoto { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}