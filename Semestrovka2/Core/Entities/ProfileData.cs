using Core.Entities.Common;

namespace Core.Entities
{
    public class ProfileData : BaseAuditableEntity
    {
        public Guid? UserId { get; set; }
        public User? User { get; set; }
        public string BackgroundImage { get; set; } = "/assets/images/banner/profile-banner.jpg";
        public string? AboutMe { get; set; } = String.Empty;
        public string? WorkingZone { get; set; } = String.Empty;
        public string? Education { get; set; } = String.Empty;
        public string? Contacts { get; set; } = String.Empty;
        public string? FacebookLink { get; set; } = String.Empty;
        public string? TwitterLink { get; set; } = String.Empty;
        public string? GoogleLink { get; set; } = String.Empty;
        public string? PinterestLink { get; set; } = String.Empty;
    }
}