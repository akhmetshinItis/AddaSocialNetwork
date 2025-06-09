using Core.Entities.Common;

namespace Core.Entities
{
    public class Album : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public List<Photo>? Photos { get; set; } = new();
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}