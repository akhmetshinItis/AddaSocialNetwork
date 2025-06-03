using Core.Entities.Common;
using ProFSB.Domain.Common.Interfaces;

namespace Core.Entities
{
    public class Message : BaseAuditableEntity
    {
        public Guid ChatId { get; set; }                  // внешний ключ на чат
        public virtual Chat Chat { get; set; }

        public Guid SenderId { get; set; }             // кто отправил
        public virtual User Sender { get; set; }

        public string Content { get; set; }              // текст сообщения
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public bool IsRead { get; set; } = false;
    }
}