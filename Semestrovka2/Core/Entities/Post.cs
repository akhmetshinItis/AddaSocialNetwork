using Core.Entities.Common;

namespace Core.Entities
{
    public class Post : BaseAuditableEntity
    {
        public Guid UserId { get; set; }

        public User? User { get; set; }
        
        public string? Content { get; set; }
        public string? Photo { get; set; }
        public List<Like> Likes { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
    }
}