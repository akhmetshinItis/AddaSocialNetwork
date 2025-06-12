namespace Contracts.Requests.AdminRequests.HobbyRequests
{
    public class GetAllHobbiesResponseHobbyItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public Guid UserId { get; set; }
        public string UserName { get; set; } = default!;
        public DateTime? CreatedDate { get; set; }
        public int PhotosCount { get; set; }
    }
} 