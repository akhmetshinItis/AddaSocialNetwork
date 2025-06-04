using Contracts.Requests.FriendsRequests.GetFriendsList;

namespace Contracts.Requests.FriendsRequests.SearchFriends
{
    public class SearchFriendsResponse
    {
        public List<GetFriendsListUserResponseItem> SearchedFriends { get; set; } = new();
    }
}