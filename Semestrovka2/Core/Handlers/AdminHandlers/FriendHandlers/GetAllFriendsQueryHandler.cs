using Contracts.Requests.AdminRequests.FriendRequests;
using Core.Abstractions;
using Core.Requests.AdminRequests.FriendRequests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Handlers.AdminHandlers.FriendHandlers;

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
            Friends = await _context.Friends
                .Select(x => new GetAllFriendsResponseFriendItem
                {
                    Id = x.Id,
                    UserId = x.User1,
                    FriendId = x.User2,
                    CreatedDate = x.CreatedDate
                }).ToListAsync(cancellationToken)
        };
} 