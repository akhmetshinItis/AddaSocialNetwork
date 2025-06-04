using Contracts.Requests.UserRequests.LoginUser;
using MediatR;

namespace Core.Requests.UserRequests.LoginUser ;

    public class LoginUserQuery : LoginUserRequest, IRequest<LoginUserResponse>
    {
    }