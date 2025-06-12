using Core.Abstractions;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class FriendsService : IFriendsService
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;
        private readonly IUserServiceIdentity _userServiceIdentity;

        public FriendsService(IDbContext dbContext, IUserContext userContext, IUserServiceIdentity userServiceIdentity)
        {
            _dbContext = dbContext;
            _userContext = userContext;
            _userServiceIdentity = userServiceIdentity;
        }

        public async Task<int> AddFriendAsync(Guid friendId)
        {
            var currentUserEmail = _userContext.GetUserEmail() ?? throw new NullReferenceException("User email is null");
            var currentUserId = _userContext.GetUserId();
            
            if(_dbContext.Friends.Any(x => x.User1 == friendId && x.User2 == currentUserId 
            || x.User1 == currentUserId && x.User2 == friendId))
                return 0;
            
            var friendship = new Friend
            {
                User1 = _userContext.GetUserId(),
                User2 = friendId,
            };

            var chat = new Chat
            {
                Id = Guid.NewGuid(),
                User1Id = friendship.User1,
                User2Id = friendship.User2,
                CreatedDate = DateTime.UtcNow,
            };

            await _dbContext.Chats.AddAsync(chat);
            await _dbContext.Friends.AddAsync(friendship);
            return await _dbContext.SaveChangesAsync();
        }

        public Task RemoveFriendAsync(Guid friendId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsFriendAsync(Guid friendId)
        {
            throw new NotImplementedException();

        }

        public IQueryable<User> GetFriends(Guid userId)
        {
            var friends =  _dbContext.Friends.Where(x => x.User1 == userId || x.User2 == userId);
            return _dbContext.Users.Where(x => friends.Any(f => 
                f.User1 == x.Id && f.User1 != userId
                || f.User2 == x.Id && f.User2 != userId));
        }
    }
}