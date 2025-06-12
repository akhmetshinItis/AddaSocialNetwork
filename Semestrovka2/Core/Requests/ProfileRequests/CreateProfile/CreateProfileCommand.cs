using MediatR;

namespace Core.Requests.ProfileRequests.CreateProfile
{
    public class CreateProfileCommand : IRequest
    {
        public Guid UserId { get; set; }
    }
} 