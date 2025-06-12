namespace Contracts.Requests.AdminRequests.HobbyRequests
{
    public class GetAllHobbiesResponse
    {
        public List<GetAllHobbiesResponseHobbyItem> Hobbies { get; set; } = new();
    }
} 