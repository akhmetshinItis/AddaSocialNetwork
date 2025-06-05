using Contracts.Requests.ProfileRequests;
using MediatR;

namespace Core.Requests.ProfileRequests
{
    public class GetProfileCommand : IRequest<GetProfileResponse>
    {
        public bool IsCurrentUserProfile { get; set; } = false;
        public Guid? UserId { get; set; }
    }
}