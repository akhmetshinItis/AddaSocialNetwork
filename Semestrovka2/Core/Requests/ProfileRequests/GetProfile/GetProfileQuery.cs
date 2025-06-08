using Contracts.Requests.ProfileRequests;
using MediatR;

namespace Core.Requests.ProfileRequests.GetProfile
{
    public class GetProfileQuery : IRequest<GetProfileResponse>
    {
        public bool IsCurrentUserProfile { get; set; } = false;
        public Guid? UserId { get; set; }
    }
}