using MediatR;

namespace Core.Requests.AdminRequests.AlbumRequests;

public class DeleteAlbumCommand : IRequest<Contracts.Responses.AlbumResponses.DeleteAlbumResponse>
{
    public Guid AlbumId { get; set; }
} 