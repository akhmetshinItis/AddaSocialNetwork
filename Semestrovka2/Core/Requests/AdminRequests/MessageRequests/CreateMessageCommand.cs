using MediatR;

namespace Core.Requests.AdminRequests.MessageRequests;

public class CreateMessageCommand : IRequest<Contracts.Responses.MessageResponses.CreateMessageResponse>
{
    public Guid ChatId { get; set; }
    public Guid SenderId { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;
    public DateTime? Timestamp { get; set; }
} 