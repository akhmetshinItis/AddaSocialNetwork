namespace Contracts.Requests.AdminRequests.PostRequests
{
    public class GetAllPostsResponse
    {
        public List<GetAllPostsResponsePostItem> Posts { get; set; } = new();
    }
} 