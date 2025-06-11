using Core.Entities.Common;

namespace Core.Entities
{
    public class FriendCategoryLink : BaseAuditableEntity
    {
        public Guid UserId { get; set; }
        public Guid FriendId { get; set; }
        public Guid FriendCategoryId { get; set; }

        public FriendCategory? FriendCategory { get; set; }
        public User? User { get; set; }
        public User? Friend { get; set; }
    }
}