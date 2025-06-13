using Core.Abstractions;
using Core.Entities;
using MediatR;
using Contracts.Responses.ChatResponses;
using Core.Requests.AdminRequests.ChatRequests;

namespace Core.Handlers.AdminHandlers.ChatHandlers;

public class CreateChatCommandHandler : IRequestHandler<CreateChatCommand, CreateChatResponse>
{
    private readonly IDbContext _context;
    public CreateChatCommandHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<CreateChatResponse> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        var chat = new Chat
        {
            User1Id = request.User1Id,
            User2Id = request.User2Id,
            CreatedDate = DateTime.UtcNow,
        };
        _context.Chats.Add(chat);
        await _context.SaveChangesAsync(cancellationToken);
        return new CreateChatResponse { Succeeded = true, ChatId = chat.Id };
    }
} 