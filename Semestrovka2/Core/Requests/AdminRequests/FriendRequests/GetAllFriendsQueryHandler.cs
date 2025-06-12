using Contracts.Requests.AdminRequests.FriendRequests;
using Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.AdminRequests.FriendRequests
{
    public class GetAllFriendsQueryHandler : IRequestHandler<GetAllFriendsQuery, GetAllFriendsResponse>
    {
        private readonly IDbContext _context;

        public GetAllFriendsQueryHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<GetAllFriendsResponse> Handle(GetAllFriendsQuery request, CancellationToken cancellationToken)
            => new GetAllFriendsResponse
            {
                Friends = await _context.FriendCategoryLinks
                    .Select(x => new GetAllFriendsResponseFriendItem
                    {
                        Id = x.Id,
                        UserId = x.UserId,
                        UserName = x.User.FirstName + " " + x.User.LastName,
                        FriendId = x.FriendId,
                        FriendName = x.Friend.FirstName + " " + x.Friend.LastName,
                        CategoryName = x.FriendCategory.CategoryName,
                        CreatedDate = x.CreatedDate
                    })
                    .ToListAsync(cancellationToken)
            };
    }
} 