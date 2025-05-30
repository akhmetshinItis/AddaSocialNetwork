using Contracts.Requests.FooterFriendsSectionRequests.GetFriendsList;

namespace Contracts.Requests.FooterFriendsSectionRequests.AddFriend
{
    public class AddFriendResponse : GetFriendsListUserResponseItem
    {
        public int RowsAffected { get; set; }

        public AddFriendResponse(int rowsAffected)
        {
            RowsAffected = rowsAffected;
        }
    }
}