using Contracts.Requests.FriendsRequests;
using Core.Abstractions;
using Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.FriendsRequests
{
    public class AddFriendCategoryCommandHandler : IRequestHandler<AddFriendCategoryRequest>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;

        public AddFriendCategoryCommandHandler(IDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task Handle(AddFriendCategoryRequest request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetUserId();
            
            // Проверяем существование категории
            var category = await _dbContext.FriendCategories
                .FirstOrDefaultAsync(c => c.CategoryName == request.Category, cancellationToken);

            if (category == null)
            {
                category = new FriendCategory
                {
                    CategoryName = request.Category
                };
                _dbContext.FriendCategories.Add(category);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            // Проверяем существование связи
            var existingLink = await _dbContext.FriendCategoryLinks
                .FirstOrDefaultAsync(l => 
                    l.CategoryId == category.Id && 
                    l.UserId == userId && 
                    l.FriendId == request.FriendId, 
                    cancellationToken);

            if (existingLink == null)
            {
                var link = new FriendCategoryLink
                {
                    CategoryId = category.Id,
                    UserId = userId,
                    FriendId = request.FriendId
                };
                _dbContext.FriendCategoryLinks.Add(link);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
} 