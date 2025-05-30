using Contracts.Requests.FriendsRequests.AddFriend;
using Core.Abstractions;
using MediatR;

namespace Core.Requests.FooterFriendsSectionRequests.AddFriend
{
    public class AddFriendCommandHandler : IRequestHandler<AddFriendCommand, AddFriendResponse>
    {
        private readonly IFriendsService _friendsService;

        public AddFriendCommandHandler(IFriendsService friendsService)
        {
            _friendsService = friendsService;
        }

        public async Task<AddFriendResponse> Handle(AddFriendCommand request, CancellationToken cancellationToken)
        {
            if(request.FriendId == Guid.Empty)
                throw new ArgumentNullException(nameof(request.FriendId));
            
            return new AddFriendResponse(await _friendsService.AddFriendAsync(request.FriendId));
        }
    }
}