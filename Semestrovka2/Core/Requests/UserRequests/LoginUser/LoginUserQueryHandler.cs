using Contracts.Requests.UserRequests.LoginUser;
using Core.Abstractions;
using MediatR;

namespace Core.Requests.UserRequests.LoginUser ;

    /// <summary>
    /// Обработчик команды <see cref="LoginUserQuery"/>
    /// </summary>
    public class LoginUserQueryHandler
        : IRequestHandler<LoginUserQuery, LoginUserResponse>
    {
        private readonly IUserServiceIdentity _userServiceIdentity;
        
        public LoginUserQueryHandler(IUserServiceIdentity userServiceIdentity)
        {
            _userServiceIdentity = userServiceIdentity;
        }
        
        public async Task<LoginUserResponse> Handle(LoginUserQuery request, CancellationToken cancellationToken)
         => new LoginUserResponse(await _userServiceIdentity.LoginAsync(request.Email, request.Password));
    }