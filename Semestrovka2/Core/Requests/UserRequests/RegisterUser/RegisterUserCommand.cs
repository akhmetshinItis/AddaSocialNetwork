using Contracts.Requests.UserRequests.RegisterUser;
using MediatR;

namespace Core.Requests.UserRequests.RegisterUser ;

    public class RegisterUserCommand : RegisterUserRequest, IRequest<RegisterUserResponse>
    {
    }