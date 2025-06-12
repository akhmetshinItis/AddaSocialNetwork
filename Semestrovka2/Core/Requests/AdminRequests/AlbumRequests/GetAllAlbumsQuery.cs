using Contracts.Requests.AdminRequests.AlbumRequests;
using MediatR;

namespace Core.Requests.AdminRequests.AlbumRequests
{
    public class GetAllAlbumsQuery : IRequest<GetAllAlbumsResponse>
    {
    }
} 