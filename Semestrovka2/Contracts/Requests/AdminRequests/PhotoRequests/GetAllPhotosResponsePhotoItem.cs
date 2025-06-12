namespace Contracts.Requests.AdminRequests.PhotoRequests
{
    public class GetAllPhotosResponsePhotoItem
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? AlbumId { get; set; }
        public string? AlbumName { get; set; }
        public string Path { get; set; } = default!;
        public DateTime? CreatedDate { get; set; }
    }
} 