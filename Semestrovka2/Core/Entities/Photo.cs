using Core.Entities.Common;

namespace Core.Entities
{
    public class Photo : BaseAuditableEntity
    {
        public string Path { get; set; }
        public Album? Album { get; set; }
        public Guid AlbumId { get; set; }
    }
}