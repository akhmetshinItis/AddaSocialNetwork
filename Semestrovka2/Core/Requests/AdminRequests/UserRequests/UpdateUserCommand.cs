using MediatR;

namespace Core.Requests.AdminRequests.UserRequests
{
    public class UpdateUserCommand : IRequest
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public bool Gender { get; set; }
        public int Age { get; set; }
        public string Country { get; set; } = default!;
    }

}