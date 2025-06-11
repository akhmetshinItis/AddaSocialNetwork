using System.Security.AccessControl;

namespace Contracts.Requests.FriendsRequests.GetFriendsList
{
    public class GetFriendsListUserResponseItem
    {
        public Guid Id { get; set; }
        public string? ImageUrl { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? CategoryName { get; set; }
    }
}