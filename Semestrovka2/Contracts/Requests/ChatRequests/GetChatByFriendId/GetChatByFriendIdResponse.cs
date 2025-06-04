using Contracts.Requests.ChatRequests.GetAllChats;

namespace Contracts.Requests.ChatRequests.GetChatByFriendId
{
    public class GetChatByFriendIdResponse
    {
        public string FriendName { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = "assets/images/profile/profile-small-15.jpg";
        public Guid FriendId { get; set; }
        public GetAllChatsResponseItem Chat { get; set; } = new();
        public List<GetAllChatsMessageResponseItem> Messages { get; set; } = new();
    }
}