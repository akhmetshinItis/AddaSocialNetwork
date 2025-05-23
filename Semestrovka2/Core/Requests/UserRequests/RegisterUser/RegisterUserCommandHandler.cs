using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Contracts.Requests.UserRequests.RegisterUser;
using Core.Abstractions;
using Core.Entities;
using MediatR;
using ProFSB.Application.Features.Users.Commands.RegisterUser;
using ProFSB.Domain.Constants;

namespace Core.Requests.UserRequests.RegisterUser ;

    /// <summary>
    /// Обработчик запроса <see cref="RegisterUserCommand"/>
    /// </summary>
    public class RegisterUserCommandHandler
    : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        
        public RegisterUserCommandHandler(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }
        
        public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var isUserExists = await _userService.FindUserByEmailAsync(request.Email);

            if (isUserExists != null)
            {
                throw new ValidationException("Пользователь с такой почтой уже существует");
            }
            
            var user = new User
            {
                Email = request.Email,
                UserName = request.Email,
            };
            
            var result = await _userService.RegisterUserAsync(user, request.Password);
            

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, RoleConstants.User)
            };

            if (result.Succeeded)
            {
                await _userService.AddClaimsAsync(user, claims);
                await _userService.LoginAsync(request.Email, request.Password);
            }

           await _emailService.SendEmailAsync(request.Email, "Спасибо за регистрацию!",
                "Вы успешно зарегистрированы на сайте");
            
            return new RegisterUserResponse(result);
        }
    }