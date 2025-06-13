using MediatR;

namespace Core.Requests.AdminRequests.HobbyRequests;

public class CreateHobbyCommand : IRequest<Contracts.Responses.HobbyResponses.CreateHobbyResponse>
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
} 