using Core.Entities.Common;

namespace Core.Entities
{
    public class Like : BaseAuditableEntity
    {
        public Guid UserId { get; set; }
    }
}