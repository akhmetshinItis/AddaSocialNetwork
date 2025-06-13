using MediatR;

namespace Core.Requests.AdminRequests.ChatRequests;

public class UpdateChatCommand : IRequest<Contracts.Responses.ChatResponses.UpdateChatResponse>
{
    public Guid Id { get; set; }
    public Guid User1Id { get; set; }
    public Guid User2Id { get; set; }
} 