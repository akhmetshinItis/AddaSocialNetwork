using Contracts.Requests.AdminRequests.UserRequests;
using Core.Abstractions;
using MediatR;

namespace Core.Requests.AdminRequests.UserRequests
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
    {
        private readonly IUserServiceIdentity _userService;
        private readonly IDbContext _dbContext;

        public DeleteUserCommandHandler(IUserServiceIdentity userService, IDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }

        public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == request.UserId);
            user.IsDeleted = true;
            var result = await _userService.DeleteUserAsync(user.IdentityUserId);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new DeleteUserResponse
            {
                Succeeded = result.Succeeded,
            };
        }
    }
}