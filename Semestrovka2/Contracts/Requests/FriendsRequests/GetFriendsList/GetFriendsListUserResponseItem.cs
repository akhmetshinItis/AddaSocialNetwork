namespace Contracts.Requests.FriendsRequests.GetFriendsList
{
    public class GetFriendsListUserResponseItem
    {
        public Guid Id { get; set; }
        public string? ImageUrl { get; set; } = default!;
        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;
        // На будущее
        // public Dictionary<string, string> MessageHsitory { get; set; } = new();
    }
}