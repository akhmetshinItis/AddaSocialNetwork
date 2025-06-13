using Core.Abstractions;
using Core.Entities;
using MediatR;
using Contracts.Responses.MessageResponses;
using Core.Requests.AdminRequests.MessageRequests;
using Microsoft.EntityFrameworkCore;

namespace Core.Handlers.AdminHandlers.MessageHandlers;

public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand, UpdateMessageResponse>
{
    private readonly IDbContext _context;
    public UpdateMessageCommandHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateMessageResponse> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);
        if (message == null)
            return new UpdateMessageResponse { Succeeded = false, Message = "Message not found" };
        message.ChatId = request.ChatId;
        message.SenderId = request.SenderId;
        message.Content = request.Content;
        message.IsRead = request.IsRead;
        message.Timestamp = request.Timestamp ?? message.Timestamp;
        await _context.SaveChangesAsync(cancellationToken);
        return new UpdateMessageResponse { Succeeded = true };
    }
} 