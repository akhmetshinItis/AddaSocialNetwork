using Contracts.Requests.FooterFriendsSectionRequests.SearchFriends;
using MediatR;

namespace Core.Requests.FooterFriendsSectionRequests.SearchFriends
{
    public class SearchFriendsQuery : IRequest<SearchFriendsResponse>
    {
        public string SearchString { get; set; } = string.Empty;

        public SearchFriendsQuery(string searchString)
        {
            SearchString = searchString;
        }
    }
}