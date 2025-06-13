using Core.Abstractions;
using Core.Entities;
using MediatR;
using Contracts.Responses.ChatResponses;
using Core.Requests.AdminRequests.ChatRequests;
using Microsoft.EntityFrameworkCore;

namespace Core.Handlers.AdminHandlers.ChatHandlers;

public class UpdateChatCommandHandler : IRequestHandler<UpdateChatCommand, UpdateChatResponse>
{
    private readonly IDbContext _context;
    public UpdateChatCommandHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateChatResponse> Handle(UpdateChatCommand request, CancellationToken cancellationToken)
    {
        var chat = await _context.Chats.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (chat == null)
            return new UpdateChatResponse { Succeeded = false, Message = "Chat not found" };
        chat.User1Id = request.User1Id;
        chat.User2Id = request.User2Id;
        await _context.SaveChangesAsync(cancellationToken);
        return new UpdateChatResponse { Succeeded = true };
    }
} 