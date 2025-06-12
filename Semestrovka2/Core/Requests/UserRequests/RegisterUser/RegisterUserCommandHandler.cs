using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Transactions;
using Contracts.Requests.UserRequests.RegisterUser;
using Core.Abstractions;
using Core.Constants;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Core.Requests.UserRequests.RegisterUser ;

    /// <summary>
    /// Обработчик запроса <see cref="RegisterUserCommand"/>
    /// </summary>
    public class RegisterUserCommandHandler
    : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
    {
        private readonly IUserServiceIdentity _userServiceIdentity;
        private readonly IEmailService _emailService;
        private readonly IDbContext _dbContext;
        
        public RegisterUserCommandHandler(IUserServiceIdentity userServiceIdentity, IEmailService emailService, IDbContext dbContext)
        {
            _userServiceIdentity = userServiceIdentity;
            _emailService = emailService;
            _dbContext = dbContext;
        }
        
        public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var isUserExists = await _userServiceIdentity.FindUserByEmailAsync(request.Email);

            if (isUserExists != null)
            {
                throw new ValidationException("Пользователь с такой почтой уже существует");
            }
            
            var user = new IdentityUser<Guid>
            {
                UserName = request.Email,
                Email = request.Email,
            };
            
            var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var result = await _userServiceIdentity.RegisterUserAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    // Не создаем доп. записи, если регистрация неуспешна
                    return new RegisterUserResponse(result);
                }

                var biznesUserId = Guid.NewGuid();

                _dbContext.Users.Add(new User
                {
                    Id = biznesUserId,
                    IdentityUserId = user.Id,
                    LastName = request.LastName,
                    FirstName = request.FirstName,
                    Age = request.Age,
                    Country = request.Country,
                    CreatedDate = DateTime.UtcNow,
                });

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, RoleConstants.User),
                    new Claim(ClaimTypes.Authentication, biznesUserId.ToString())
                };

                await _userServiceIdentity.AddClaimsAsync(user, claims);
                await _userServiceIdentity.LoginAsync(request.Email, request.Password);
                await _dbContext.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                // НА ВРЕМЯ ТЕСТОВ
                // await _emailService.SendEmailAsync(request.Email, "Спасибо за регистрацию!",
                //     "Вы успешно зарегистрированы на сайте");

                return new RegisterUserResponse(result);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw new Exception("Ошибка при регистрации пользователя", ex);
            }
        }
    }