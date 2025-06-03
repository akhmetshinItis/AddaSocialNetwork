using Contracts.Requests.ChatRequests.GetAllChats;

namespace Contracts.Requests.ChatRequests.GetChatByFriendId
{
    public class GetChatByFriendIdResponse
    {
        public GetAllChatsResponseItem Chat { get; set; } = new();
        public List<GetAllChatsMessageResponseItem> Messages { get; set; } = new();
    }
}