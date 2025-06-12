namespace Contracts.Requests.AdminRequests.FriendRequests
{
    public class GetAllFriendsResponseFriendItem
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = default!;
        public Guid FriendId { get; set; }
        public string FriendName { get; set; } = default!;
        public string? CategoryName { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
} 