namespace Contracts.Responses.HobbyResponses;

public class CreateHobbyResponse
{
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public Guid? HobbyId { get; set; }
} 