namespace Contracts.Requests.FriendsRequests.GetFriendsList
{
    public class GetFriendsListResponse
    {
        public List<GetFriendsListUserResponseItem> FriendsList { get; set; }
        public Guid UserId { get; set; }
        
        public GetFriendsListResponse(List<GetFriendsListUserResponseItem> friendsList, Guid userId)
            => (FriendsList, UserId) = (friendsList, userId);
    }
}