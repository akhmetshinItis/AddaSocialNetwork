namespace Contracts.Requests.AdminRequests.ChatRequests
{
    public class GetAllChatsResponse
    {
        public List<GetAllChatsResponseChatItem> Chats { get; set; } = new();
    }
} 