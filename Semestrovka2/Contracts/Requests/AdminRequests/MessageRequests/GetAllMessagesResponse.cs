namespace Contracts.Requests.AdminRequests.MessageRequests
{
    public class GetAllMessagesResponse
    {
        public List<GetAllMessagesResponseMessageItem> Messages { get; set; } = new();
    }
} 