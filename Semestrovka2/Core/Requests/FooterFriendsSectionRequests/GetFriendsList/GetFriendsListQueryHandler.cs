using Contracts.Requests.FooterFriendsSectionRequests;
using Contracts.Requests.FooterFriendsSectionRequests.GetFriendsList;
using Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.FooterFriendsSectionRequests.GetFriendsList
{
    public class GetFrinendsListQueryHandler : IRequestHandler<GetFriendsListQuery, GetFriendsListResponse>
    {
        private readonly IFriendsService _friendsService;

        public GetFrinendsListQueryHandler(IFriendsService friendsService)
        {
            _friendsService = friendsService;
        }
        
        public async Task<GetFriendsListResponse> Handle(GetFriendsListQuery request, CancellationToken cancellationToken)
        {
            var friends = await _friendsService.GetFriends().Select(x 
                => new GetFriendsListUserResponseItem
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    ImageUrl = x.ImageUrl,
                }).ToListAsync(cancellationToken: cancellationToken);
            return new GetFriendsListResponse(friends);
        }
    }
}