using Core.Entities.Common;

namespace Core.Entities
{
    public class Comment : BaseAuditableEntity
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string Content { get; set; } = string.Empty;
        public Guid PostId { get; set; }
        public Post? Post { get; set; }
    }
}