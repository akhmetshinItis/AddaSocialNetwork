using MediatR;

namespace Core.Requests.AdminRequests.ChatRequests;

public class CreateChatCommand : IRequest<Contracts.Responses.ChatResponses.CreateChatResponse>
{
    public Guid User1Id { get; set; }
    public Guid User2Id { get; set; }
} 