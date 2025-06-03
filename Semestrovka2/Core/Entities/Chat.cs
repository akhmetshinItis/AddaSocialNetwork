using Core.Entities.Common;

namespace Core.Entities
{
    public class Chat : BaseAuditableEntity
    {
        // Участники чата
        public Guid User1Id { get; set; }
        public Guid User2Id { get; set; }

        // Навигационные свойства (при необходимости)
        public virtual User User1 { get; set; }
        public virtual User User2 { get; set; }

        public List<Message> Messages { get; set; } = new();

        public Message LastMessage { get; set; } = new();
    }

}