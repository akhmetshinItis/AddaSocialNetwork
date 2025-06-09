using Core.Entities.Common;

namespace Core.Entities
{
    public class HobbyCategory : BaseEntity
    {
        public Guid UserId{ get; set; }
        public string Name { get; set; } = default!;
        public List<string> Photos { get; set; } = new();
    }
}