using MediatR;

namespace Core.Requests.AdminRequests.HobbyRequests;

public class UpdateHobbyCommand : IRequest<Contracts.Responses.HobbyResponses.UpdateHobbyResponse>
{
    public Guid HobbyId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
} 