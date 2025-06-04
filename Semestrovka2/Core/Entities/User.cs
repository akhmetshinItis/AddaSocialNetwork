using Core.Entities.Common;

namespace Core.Entities
{
    public class User : BaseAuditableEntity
    {
        public string LastName { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public bool Gender { get; set; } = default!;
        public int Age { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string? ImageUrl { get; set; } = "default";
        public Guid IdentityUserId { get; set; } = default!;
    }
}