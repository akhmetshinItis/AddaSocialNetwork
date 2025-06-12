namespace Contracts.Requests.PhotoRequests
{
    public class GetAlbumsResponse
    {
        public List<AlbumResponseItem>? Albums { get; set; } = new();
        public Guid UserId { get; set; }
        public bool IsCurrentUserProfile { get; set; }
    }
}