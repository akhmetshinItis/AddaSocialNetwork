using Core.Entities.Common;

namespace Core.Entities
{
    public class Friend : BaseAuditableEntity
    {
        public Guid User1 { get; set; } = new();
        public Guid User2 { get; set; } = new();
    }
}