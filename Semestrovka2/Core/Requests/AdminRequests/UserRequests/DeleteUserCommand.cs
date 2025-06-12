using Contracts.Requests.AdminRequests.UserRequests;
using MediatR;

namespace Core.Requests.AdminRequests.UserRequests;

    public class DeleteUserCommand : IRequest<DeleteUserResponse>
    {
        public Guid UserId { get; set; }
    }