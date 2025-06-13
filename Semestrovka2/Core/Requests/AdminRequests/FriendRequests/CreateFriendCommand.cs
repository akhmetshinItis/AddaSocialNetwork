using MediatR;

namespace Core.Requests.AdminRequests.FriendRequests;

public class CreateFriendCommand : IRequest<Contracts.Responses.FriendResponses.CreateFriendResponse>
{
    public Guid User1 { get; set; }
    public Guid User2 { get; set; }
} 