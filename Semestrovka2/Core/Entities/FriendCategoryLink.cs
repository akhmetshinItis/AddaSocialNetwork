using Core.Entities.Common;

namespace Core.Entities
{
    public class FriendCategoryLink : BaseAuditableEntity
    {
        public string UserId { get; set; }
        public string FriendId { get; set; }
        public int CategoryId { get; set; }

        public FriendCategory FriendCategory { get; set; }
    }
}