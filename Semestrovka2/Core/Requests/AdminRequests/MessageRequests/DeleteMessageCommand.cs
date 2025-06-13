using MediatR;

namespace Core.Requests.AdminRequests.MessageRequests;

public class DeleteMessageCommand : IRequest<Contracts.Responses.MessageResponses.DeleteMessageResponse>
{
    public Guid Id { get; set; }
} 