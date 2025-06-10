using Contracts.Requests.FriendsRequests.GetFriendsList;
using Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.FooterFriendsSectionRequests.GetFriendsList
{
    public class GetFrinendsListQueryHandler : IRequestHandler<GetFriendsListQuery, GetFriendsListResponse>
    {
        private readonly IFriendsService _friendsService;
        private readonly IUserContext _userContext;

        public GetFrinendsListQueryHandler(IFriendsService friendsService, IUserContext userContext)
        {
            _friendsService = friendsService;
            _userContext = userContext;
        }
        
        public async Task<GetFriendsListResponse> Handle(GetFriendsListQuery request, CancellationToken cancellationToken)
        {
            var userId = request.UserId ?? _userContext.GetUserId();
            var friends = await _friendsService.GetFriends(userId).Select(x 
                => new GetFriendsListUserResponseItem
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ImageUrl = x.ImageUrl,
                }).ToListAsync(cancellationToken: cancellationToken);
            return new GetFriendsListResponse(friends, userId);
        }
    }
}