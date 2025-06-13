using Contracts.Requests.AdminRequests.PhotoRequests;
using MediatR;

namespace Core.Requests.AdminRequests.PhotoRequests
{
    public class GetPhotoByIdQuery : IRequest<GetAllPhotosResponsePhotoItem>
    {
        public Guid Id { get; set; }
    }
} 