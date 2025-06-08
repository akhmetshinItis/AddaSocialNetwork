using Contracts.Requests.FriendsRequests.GetFriendsList;

namespace Contracts.Requests.HomePageRequests
{
    public class GetHomePageResponse
    {
        public List<PostResponseItem> Posts { get; set; }
        public HomeProfileResponseItem HomeProfileResponseItem { get; set; }
        public List<GetFriendsListUserResponseItem> FriendsList { get; set; }
    }
}