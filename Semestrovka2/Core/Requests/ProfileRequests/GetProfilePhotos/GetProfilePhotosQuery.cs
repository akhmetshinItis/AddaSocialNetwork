using Contracts.Requests.ProfileRequests;
using MediatR;

namespace Core.Requests.ProfileRequests.GetProfilePhotos
{
    public class GetProfilePhotosQuery : IRequest<GetProfilePhotosResponse>
    {
        public Guid? UserId { get; set;} 
    }
}