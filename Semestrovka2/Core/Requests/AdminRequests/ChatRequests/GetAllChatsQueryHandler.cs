using Contracts.Requests.AdminRequests.ChatRequests;
using Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.AdminRequests.ChatRequests
{
    public class GetAllChatsQueryHandler : IRequestHandler<GetAllChatsQuery, GetAllChatsResponse>
    {
        private readonly IDbContext _context;

        public GetAllChatsQueryHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<GetAllChatsResponse> Handle(GetAllChatsQuery request, CancellationToken cancellationToken)
            => new GetAllChatsResponse
            {
                Chats = await _context.Chats
                    .Select(x => new GetAllChatsResponseChatItem
                    {
                        Id = x.Id,
                        User1Id = x.User1Id,
                        User1Name = x.User1.FirstName + " " + x.User1.LastName,
                        User2Id = x.User2Id,
                        User2Name = x.User2.FirstName + " " + x.User2.LastName,
                        MessagesCount = x.Messages.Count,
                        LastMessageContent = x.LastMessage.Content,
                        LastMessageTime = x.LastMessage.Timestamp,
                        CreatedDate = x.CreatedDate
                    })
                    .ToListAsync(cancellationToken)
            };
    }
} 