using Contracts.Requests.AdminRequests.FriendCategoriesRequests;
using Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.AdminRequests.FriendCategoryRequests
{
    public class GetAllFriendCategoriesQueryHandler : IRequestHandler<GetAllFriendCategoriesQuery, GetAllFriendCategoriesResponse>
    {
        private readonly IDbContext _context;

        public GetAllFriendCategoriesQueryHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<GetAllFriendCategoriesResponse> Handle(GetAllFriendCategoriesQuery request, CancellationToken cancellationToken)
        {
            return new GetAllFriendCategoriesResponse
            {
                Categories = await _context.FriendCategoryLinks.Select(x => new GetAllFriendCategoriesResponseItem
                {
                    Id = x.Id,
                    FriendCategoryId = x.FriendCategoryId,
                    FriendId = x.FriendId,
                    Name = x.FriendCategory.CategoryName,
                    UserId = x.UserId,
                }).ToListAsync()
            };
        }
    }
}