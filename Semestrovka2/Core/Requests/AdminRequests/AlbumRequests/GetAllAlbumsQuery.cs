using MediatR;

namespace Core.Requests.AdminRequests.AlbumRequests;

public class GetAllAlbumsQuery : IRequest<Contracts.Requests.AdminRequests.AlbumRequests.GetAllAlbumsResponse>
{
} 