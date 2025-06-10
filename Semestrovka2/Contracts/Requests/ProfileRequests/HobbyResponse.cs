using Contracts.Requests.PhotoRequests;

namespace Contracts.Requests.ProfileRequests
{
    public class HobbyResponse
    {
        public string Name { get; set; }
        public List<PhotoResponseItem> Photos { get; set; }
    }
}