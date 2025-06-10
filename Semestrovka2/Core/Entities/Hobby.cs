using Core.Entities.Common;

namespace Core.Entities
{
    public class Hobby : BaseAuditableEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public ICollection<HobbyPhoto> Photos { get; set; } = new List<HobbyPhoto>();
    }
} 