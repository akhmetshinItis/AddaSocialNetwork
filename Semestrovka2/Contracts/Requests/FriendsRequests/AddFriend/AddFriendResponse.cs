using Contracts.Requests.FriendsRequests.GetFriendsList;

namespace Contracts.Requests.FriendsRequests.AddFriend
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