namespace Contracts.Responses.MessageResponses;

public class CreateMessageResponse
{
    public bool Succeeded { get; set; }
    public Guid MessageId { get; set; }
    public string? Message { get; set; }
} 