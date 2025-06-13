namespace Contracts.Requests.AdminRequests.AlbumRequests;

public class CreateAlbumRequest
{
    public string Name { get; set; }
    public Guid UserId { get; set; }
} 