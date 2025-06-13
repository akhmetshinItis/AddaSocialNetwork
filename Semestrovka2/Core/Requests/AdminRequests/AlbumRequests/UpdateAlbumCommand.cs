using MediatR;

namespace Core.Requests.AdminRequests.AlbumRequests;

public class UpdateAlbumCommand : IRequest<Contracts.Responses.AlbumResponses.UpdateAlbumResponse>
{
    public Guid AlbumId { get; set; }
    public string Name { get; set; }
    public Guid UserId { get; set; }
} 