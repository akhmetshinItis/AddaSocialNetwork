namespace Contracts.Requests.AdminRequests.CommentRequests
{
    public class GetAllCommentsResponse
    {
        public List<GetAllCommentsResponseCommentItem> Comments { get; set; } = new();
    }
} 