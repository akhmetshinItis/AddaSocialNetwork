using Contracts.Requests.FooterFriendsSectionRequests.GetFriendsList;

namespace Contracts.Requests.FooterFriendsSectionRequests.SearchFriends
{
    public class SearchFriendsResponse
    {
        public List<GetFriendsListUserResponseItem> SearchedFriends { get; set; } = new();
    }
}