using Core.Abstractions;
using Core.Entities;
using MediatR;
using Contracts.Responses.MessageResponses;
using Core.Requests.AdminRequests.MessageRequests;
using Microsoft.EntityFrameworkCore;

namespace Core.Handlers.AdminHandlers.MessageHandlers;

public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, DeleteMessageResponse>
{
    private readonly IDbContext _context;
    public DeleteMessageCommandHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<DeleteMessageResponse> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);
        if (message == null)
            return new DeleteMessageResponse { Succeeded = false, Message = "Message not found" };
        message.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);
        return new DeleteMessageResponse { Succeeded = true };
    }
} 