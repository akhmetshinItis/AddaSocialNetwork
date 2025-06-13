namespace Contracts.Responses.ChatResponses;

public class CreateChatResponse
{
    public bool Succeeded { get; set; }
    public Guid ChatId { get; set; }
    public string? Message { get; set; }
} 