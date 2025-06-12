using Core.Entities.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Core.Entities
{
    public class User : BaseAuditableEntity
    {
        public string LastName { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        
        public string Email { get; set; } = string.Empty;
        
        public bool Gender { get; set; } = default!;
        public int Age { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string? ImageUrl { get; set; } = "/assets/images/profile/profile-1.jpg";
        public Guid IdentityUserId { get; set; } = default!;
        public ICollection<Hobby> Hobbies { get; set; } = new List<Hobby>();
        public FriendCategoryLink FriendCategoryLink { get; set; }
    }
}