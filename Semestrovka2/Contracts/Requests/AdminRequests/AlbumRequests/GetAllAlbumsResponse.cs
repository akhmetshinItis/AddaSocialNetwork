namespace Contracts.Requests.AdminRequests.AlbumRequests
{
    public class GetAllAlbumsResponse
    {
        public List<GetAllAlbumsResponseAlbumItem> Albums { get; set; } = new();
    }
} 