using MediatR;

namespace Core.Requests.AdminRequests.FriendRequests;

public class UpdateFriendCommand : IRequest<Contracts.Responses.FriendResponses.UpdateFriendResponse>
{
    public Guid FriendId { get; set; }
    public Guid User1 { get; set; }
    public Guid User2 { get; set; }
} 