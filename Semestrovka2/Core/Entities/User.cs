using Core.Entities.Common;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class User : BaseAuditableEntity
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string Country { get; set; }

        public Guid IdentityUserId { get; set; }
    }
}