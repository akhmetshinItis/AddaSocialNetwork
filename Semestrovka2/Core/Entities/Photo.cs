using Core.Entities.Common;

namespace Core.Entities
{
    public class Photo : BaseAuditableEntity
    {
        public string Path { get; set; }
    }
}