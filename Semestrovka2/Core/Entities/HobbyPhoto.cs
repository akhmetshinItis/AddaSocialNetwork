using Core.Entities.Common;

namespace Core.Entities
{
    public class HobbyPhoto : BaseAuditableEntity
    {
        public Guid HobbyId { get; set; }
        public Hobby Hobby { get; set; } = null!;
        public string Path { get; set; } = string.Empty;
    }
} 