using Contracts.Requests.ChatRequests.GetAllChats;
using Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.ChatRequests.GetAllChats
{
    public class GetAllChatsQueryHandler : IRequestHandler<GetAllChatsQuery, GetAllChatsResponse>
    {
        private readonly IDbContext _context;
        private readonly IUserContext _userContext;

        public GetAllChatsQueryHandler(IDbContext context, IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<GetAllChatsResponse> Handle(GetAllChatsQuery request, CancellationToken cancellationToken)
        {
            return new GetAllChatsResponse
            {
                Chats = await _context.Chats
                    .Where(x => x.User1Id == _userContext.GetUserId())
                    .Select(x => new GetAllChatsResponseItem
                    {
                        User1Id = x.User1Id,
                        User2Id = x.User2Id,
                        LastMessage = new GetAllChatsMessageResponseItem
                        {
                            ChatId = x.Id,
                            Content = x.LastMessage.Content,
                            SenderId = x.LastMessage.SenderId,
                            IsRead = x.LastMessage.IsRead,
                            Timestamp = x.LastMessage.Timestamp
                        },
                    }).ToListAsync(cancellationToken: cancellationToken)
            };
        }
    }
}