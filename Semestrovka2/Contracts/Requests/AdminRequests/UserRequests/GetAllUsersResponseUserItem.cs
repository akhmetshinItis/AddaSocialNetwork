namespace Contracts.Requests.AdminRequests.UserRequests
{
    public class GetAllUsersResponseUserItem
    {
        public Guid Id { get; set; }
        public string? Email { get; set; } = String.Empty;
        public string LastName { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string Gender { get; set; } = default!;
        public int Age { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string? ImageUrl { get; set; } = "/assets/images/profile/profile-1.jpg";
        public Guid IdentityUserId { get; set; } = default!;
        public DateTime? CreatedDate { get; set; }
    }
}