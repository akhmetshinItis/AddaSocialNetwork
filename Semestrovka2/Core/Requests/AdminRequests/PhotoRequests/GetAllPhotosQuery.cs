using Contracts.Requests.AdminRequests.PhotoRequests;
using MediatR;

namespace Core.Requests.AdminRequests.PhotoRequests
{
    public class GetAllPhotosQuery : IRequest<GetAllPhotosResponse>
    {
    }
} 