namespace Contracts.Requests.FriendsRequests.GetFriendsList
{
    public class GetFriendsListResponse
    {
        public List<GetFriendsListUserResponseItem> FriendsList { get; set; }
        
        public GetFriendsListResponse(List<GetFriendsListUserResponseItem> friendsList)
            => (FriendsList) = (friendsList);
    }
}