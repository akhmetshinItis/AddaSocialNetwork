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
        private readonly IUserService _userService;
        
        public LoginUserQueryHandler(IUserService userService)
        {
            _userService = userService;
        }
        
        public async Task<LoginUserResponse> Handle(LoginUserQuery request, CancellationToken cancellationToken)
         => new LoginUserResponse(await _userService.LoginAsync(request.Email, request.Password));
    }