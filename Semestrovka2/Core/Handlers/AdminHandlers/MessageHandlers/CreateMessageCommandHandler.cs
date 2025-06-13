using Core.Abstractions;
using Core.Entities;
using MediatR;
using Contracts.Responses.MessageResponses;
using Core.Requests.AdminRequests.MessageRequests;

namespace Core.Handlers.AdminHandlers.MessageHandlers;

public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, CreateMessageResponse>
{
    private readonly IDbContext _context;
    public CreateMessageCommandHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<CreateMessageResponse> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var message = new Message
        {
            ChatId = request.ChatId,
            SenderId = request.SenderId,
            Content = request.Content,
            IsRead = request.IsRead,
            Timestamp = request.Timestamp ?? DateTime.UtcNow,
            CreatedDate = DateTime.UtcNow,
        };
        _context.Messages.Add(message);
        await _context.SaveChangesAsync(cancellationToken);
        return new CreateMessageResponse { Succeeded = true, MessageId = message.Id };
    }
} 