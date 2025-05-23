using Contracts.Requests.UserRequests.RegisterUser;
using MediatR;

namespace ProFSB.Application.Features.Users.Commands.RegisterUser ;

    public class RegisterUserCommand : RegisterUserRequest, IRequest<RegisterUserResponse>
    {
    }