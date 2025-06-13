using Core.Abstractions;
using Core.Entities;
using MediatR;
using Contracts.Responses.ChatResponses;
using Core.Requests.AdminRequests.ChatRequests;
using Microsoft.EntityFrameworkCore;

namespace Core.Handlers.AdminHandlers.ChatHandlers;

public class DeleteChatCommandHandler : IRequestHandler<DeleteChatCommand, DeleteChatResponse>
{
    private readonly IDbContext _context;
    public DeleteChatCommandHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<DeleteChatResponse> Handle(DeleteChatCommand request, CancellationToken cancellationToken)
    {
        var chat = await _context.Chats.Include(c => c.Messages).FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (chat == null)
            return new DeleteChatResponse { Succeeded = false, Message = "Chat not found" };
        chat.IsDeleted = true;
        if (chat.Messages != null)
        {
            foreach (var message in chat.Messages)
                message.IsDeleted = true;
        }
        await _context.SaveChangesAsync(cancellationToken);
        return new DeleteChatResponse { Succeeded = true };
    }
} 