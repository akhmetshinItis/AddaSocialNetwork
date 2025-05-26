using System.Security.AccessControl;

namespace Contracts.Requests.FooterFriendsSectionRequests.GetFriendsList
{
    public class GetFriendsListUserResponseItem
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        
        // На будущее
        // public Dictionary<string, string> MessageHsitory { get; set; } = new();
    }
}