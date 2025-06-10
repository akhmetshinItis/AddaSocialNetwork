namespace Contracts.Requests.HomePageRequests
{
    public class CommentResponseItem
    {
        public string UserPhoto { get; set; }
        public string Content { get; set; } = string.Empty;
        public string UserName { get; set; }
    }
}