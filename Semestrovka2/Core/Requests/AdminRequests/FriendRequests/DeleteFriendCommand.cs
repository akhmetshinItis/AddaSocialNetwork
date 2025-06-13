using MediatR;

namespace Core.Requests.AdminRequests.FriendRequests;

public class DeleteFriendCommand : IRequest<Contracts.Responses.FriendResponses.DeleteFriendResponse>
{
    public Guid FriendId { get; set; }
} 