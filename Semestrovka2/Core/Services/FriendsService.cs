using Core.Abstractions;
using Core.Entities;

namespace Core.Services
{
    public class FriendsService : IFriendsService
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;
        private readonly IUserService _userService;

        public FriendsService(IDbContext dbContext, IUserContext userContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userContext = userContext;
            _userService = userService;
        }

        public async Task AddFriendAsync(Guid friendId)
        {
            var currentUserEmail = _userContext.GetUserEmail() ?? throw new NullReferenceException("User email is null");
            var currentUserId = _userService.FindUserByEmailAsync(currentUserEmail).Id;
            var friendship = new Friend
            {
                User1 = _userContext.GetUserId() ?? throw new NullReferenceException("User id is null"),
                User2 = friendId,
            };
            await _dbContext.Friends.AddAsync(friendship);
        }

        public Task RemoveFriendAsync(Guid friendId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsFriendAsync(Guid friendId)
        {
            throw new NotImplementedException();

        }

        public Task<List<User>> GetFriendsAsync()
        {
            var userId = _userContext.GetUserId();
            var friends = _dbContext.Friends.Where(x => x.User1 == userId || x.User2 == userId).ToList();
            throw new NotImplementedException();
        }
    }
}