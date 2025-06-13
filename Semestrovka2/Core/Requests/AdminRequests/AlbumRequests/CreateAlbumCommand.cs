using MediatR;

namespace Core.Requests.AdminRequests.AlbumRequests;

public class CreateAlbumCommand : IRequest<Contracts.Responses.AlbumResponses.CreateAlbumResponse>
{
    public string Name { get; set; }
    public Guid UserId { get; set; }
} 