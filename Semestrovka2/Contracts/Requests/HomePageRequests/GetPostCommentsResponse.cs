namespace Contracts.Requests.HomePageRequests
{
    public class GetPostCommentsResponse
    {
        public List<CommentResponseItem> Comments { get; set; } = new();
    }
} 