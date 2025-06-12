using Contracts.Requests.UserRequests.RegisterUser;
using MediatR;

namespace Core.Requests.AdminRequests.UserRequests
{
    public class RegisterUserCommandAdmin : RegisterUserRequest, IRequest<RegisterUserResponse>
    {
    }
}