using System.Security.AccessControl;

namespace Contracts.Requests.AdminRequests.AlbumRequests
{
    public class GetAllAlbumsResponseAlbumItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? PhotoCount { get; set; }
    }
} 