namespace Contracts.Requests.AdminRequests.FriendRequests
{
    public class GetAllFriendsResponse
    {
        public List<GetAllFriendsResponseFriendItem> Friends { get; set; } = new();
    }
} 