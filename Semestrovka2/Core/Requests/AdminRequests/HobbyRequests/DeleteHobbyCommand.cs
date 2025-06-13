using MediatR;

namespace Core.Requests.AdminRequests.HobbyRequests;

public class DeleteHobbyCommand : IRequest<Contracts.Responses.HobbyResponses.DeleteHobbyResponse>
{
    public Guid HobbyId { get; set; }
} 