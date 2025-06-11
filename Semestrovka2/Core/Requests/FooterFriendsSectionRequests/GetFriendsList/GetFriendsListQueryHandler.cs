using Contracts.Requests.FriendsRequests.GetFriendsList;
using Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.FooterFriendsSectionRequests.GetFriendsList
{
    public class GetFrinendsListQueryHandler1 : IRequestHandler<GetFriendsListQuery, GetFriendsListResponse>
    {
        private readonly IFriendsService _friendsService;
        private readonly IUserContext _userContext;
        private readonly IDbContext _dbContext;

        public GetFrinendsListQueryHandler1(IFriendsService friendsService, IUserContext userContext, IDbContext dbContext)
        {
            _friendsService = friendsService;
            _userContext = userContext;
            _dbContext = dbContext;
        }
        
        public async Task<GetFriendsListResponse> Handle(GetFriendsListQuery request, CancellationToken cancellationToken)
        {
            var userId = request.UserId ?? _userContext.GetUserId();
            
            var friendsQuery = _friendsService.GetFriends(userId);
            
            if (!string.IsNullOrEmpty(request.Category))
            {
                friendsQuery = friendsQuery.Where(friend =>
                    _dbContext.FriendCategoryLinks.Any(link =>
                        link.FriendId == friend.Id &&
                        link.UserId == userId &&
                        link.FriendCategory.CategoryName == request.Category));

            }

            var friends = await friendsQuery
                .Select(x => new GetFriendsListUserResponseItem
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ImageUrl = x.ImageUrl,
                    CategoryName = _dbContext.FriendCategoryLinks
                        .Where(l => l.UserId == userId && l.FriendId == x.Id)
                        .Select(l => l.FriendCategory.CategoryName)
                        .FirstOrDefault() ?? "Default",
                })
                .ToListAsync(cancellationToken: cancellationToken);

            return new GetFriendsListResponse(friends, userId);
        }
    }
}