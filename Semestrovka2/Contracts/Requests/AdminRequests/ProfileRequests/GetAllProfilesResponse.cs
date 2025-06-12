namespace Contracts.Requests.AdminRequests.ProfileRequests
{
    public class GetAllProfilesResponse
    {
        public List<GetAllProfilesResponseProfileItem> Profiles { get; set; } = new();
    }
} 