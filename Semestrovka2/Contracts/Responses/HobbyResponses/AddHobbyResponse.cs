namespace Contracts.Responses.HobbyResponses;

public class AddHobbyResponse
{
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public Guid? HobbyId { get; set; }
} 