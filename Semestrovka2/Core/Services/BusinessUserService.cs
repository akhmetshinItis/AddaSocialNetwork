using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class BusinessUserService : IBusinessUserService
    {
        private readonly IDbContext _dbContext;
        private readonly IFriendsService _friendsService;
        private readonly IUserContext _userContext;

        public BusinessUserService(IDbContext dbContext, IUserContext userContext, IFriendsService friendsService)
        {
            _dbContext = dbContext;
            _userContext = userContext;
            _friendsService = friendsService;
        }

        public IQueryable<User> SearchUsers(string? searchString)
        {
            var excludeIds = _friendsService.GetFriends(_userContext.GetUserId());
            if (searchString == null || searchString.Trim().Length == 0)
                return _dbContext.Users.Where(x => x.Id != _userContext.GetUserId()
                                                   && !excludeIds.Contains(x));
            
            return _dbContext.Users.Where(x => x.Id != _userContext.GetUserId() 
                && !excludeIds.Contains(x)
                && (x.FirstName.ToLower() + x.LastName.ToLower()).Contains(searchString.ToLower()));
        }

        public User GetCurrentUser()
        {
            return _dbContext.Users.FirstOrDefault(x => x.Id == _userContext.GetUserId()) ?? throw new EntityNotFoundException<User>("Не найден");
        }
    }
}