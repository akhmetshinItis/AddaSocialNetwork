using Core.Abstractions;
using MediatR;

namespace Core.Requests.AdminRequests.UserRequests
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserServiceIdentity _userServiceIdentity;

        public UpdateUserCommandHandler(IDbContext dbContext, IUserServiceIdentity userServiceIdentity)
        {
            _dbContext = dbContext;
            _userServiceIdentity = userServiceIdentity;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(new object[] { request.UserId }, cancellationToken);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            // Обновляем данные пользователя
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.Gender = request.Gender;
            user.Age = request.Age;
            user.Country = request.Country;

            // Обновляем данные в Identity
            var identityUser = await _userServiceIdentity.FindUserByEmailAsync(user.Email);
            if (identityUser != null)
            {
                identityUser.Email = request.Email;
                identityUser.UserName = request.Email;
                await _userServiceIdentity.UpdateUserAsync(identityUser);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
} 