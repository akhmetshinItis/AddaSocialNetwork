using Contracts.Requests.FooterFriendsSectionRequests.GetFriendsList;
using Contracts.Requests.FooterFriendsSectionRequests.SearchFriends;
using Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.FooterFriendsSectionRequests.SearchFriends
{
    public class SearchFriendsQueryHandler : IRequestHandler<SearchFriendsQuery, SearchFriendsResponse>
    {
        private readonly IBusinessUserService _businessUserService;

        public SearchFriendsQueryHandler(IBusinessUserService businessUserService)
        {
            _businessUserService = businessUserService;
        }

        public async Task<SearchFriendsResponse> Handle(SearchFriendsQuery request, CancellationToken cancellationToken)
            => new()
            {
                SearchedFriends = await _businessUserService.SearchUsers(request.SearchString)
                    .Select(x => new GetFriendsListUserResponseItem
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        ImageUrl = x.ImageUrl,
                    })
                    .ToListAsync(cancellationToken)
            };
    }
}