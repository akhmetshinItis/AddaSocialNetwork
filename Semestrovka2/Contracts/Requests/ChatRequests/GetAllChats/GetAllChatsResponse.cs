namespace Contracts.Requests.ChatRequests.GetAllChats
{
    public class GetAllChatsResponse
    {
        public List<GetAllChatsResponseItem> Chats { get; set; } = new();
    }
}