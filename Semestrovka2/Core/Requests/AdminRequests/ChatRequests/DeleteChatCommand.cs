using MediatR;

namespace Core.Requests.AdminRequests.ChatRequests;

public class DeleteChatCommand : IRequest<Contracts.Responses.ChatResponses.DeleteChatResponse>
{
    public Guid Id { get; set; }
} 