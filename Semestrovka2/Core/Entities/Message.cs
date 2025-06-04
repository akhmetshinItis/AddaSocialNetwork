using Core.Entities.Common;
using ProFSB.Domain.Common.Interfaces;

namespace Core.Entities
{
    public class Message : BaseAuditableEntity
    {
        public Guid? ChatId { get; set; }
        public virtual Chat Chat { get; set; }

        public Guid? SenderId { get; set; }
        public virtual User Sender { get; set; }

        public string Content { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public bool IsRead { get; set; } = false;
    }
}