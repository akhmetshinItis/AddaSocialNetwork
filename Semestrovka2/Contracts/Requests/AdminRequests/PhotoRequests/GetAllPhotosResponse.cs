namespace Contracts.Requests.AdminRequests.PhotoRequests
{
    public class GetAllPhotosResponse
    {
        public List<GetAllPhotosResponsePhotoItem> Photos { get; set; } = new();
    }
} 