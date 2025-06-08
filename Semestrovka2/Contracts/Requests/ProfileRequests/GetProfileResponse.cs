using System.Globalization;

namespace Contracts.Requests.ProfileRequests
{
    public class GetProfileResponse
    {
        public string ProfileImage { get; set; } = default!;
        public string BackgroundImage { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string AboutMe { get; set; } = String.Empty;
        public string WorkingZone { get; set; } = String.Empty;
        public string Education { get; set; } = String.Empty;
        public string Contacts { get; set; } = String.Empty;
        public string FacebookLink { get; set; } = String.Empty;
        public string TwitterLink { get; set; } = String.Empty;
        public string GoogleLink { get; set; } = String.Empty;
        public string PinterestLink { get; set; } = String.Empty;
        public bool IsCurrentUserProfile { get; set; }
    }
}