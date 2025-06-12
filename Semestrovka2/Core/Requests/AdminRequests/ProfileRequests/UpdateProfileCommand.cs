using MediatR;

namespace Core.Requests.AdminRequests.ProfileRequests
{
    public class UpdateProfileCommand : IRequest
    {
        public Guid ProfileId { get; set; }
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