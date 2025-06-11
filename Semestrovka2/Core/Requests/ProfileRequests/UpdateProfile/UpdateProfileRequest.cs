using MediatR;

namespace Core.Requests.ProfileRequests.UpdateProfile
{
    public class UpdateProfileRequest : IRequest
    {
        public string AboutMe { get; set; } = string.Empty;
        public string WorkingZone { get; set; } = string.Empty;
        public string Education { get; set; } = string.Empty;
        public string Contacts { get; set; } = string.Empty;
        public string FacebookLink { get; set; } = string.Empty;
        public string TwitterLink { get; set; } = string.Empty;
        public string GoogleLink { get; set; } = string.Empty;
        public string PinterestLink { get; set; } = string.Empty;
    }
} 