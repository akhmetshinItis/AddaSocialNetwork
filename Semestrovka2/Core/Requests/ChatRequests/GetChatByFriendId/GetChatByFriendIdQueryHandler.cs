using Contracts.Requests.ChatRequests.GetAllChats;
using Contracts.Requests.ChatRequests.GetChatByFriendId;
using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.ChatRequests.GetChatByFriendId
{
    public class GetChatByFriendIdQueryHandler : IRequestHandler<GetChatByFriendIdQuery, GetChatByFriendIdResponse>
    {
        private readonly IDbContext _context;
        private readonly IUserContext _userContext;

        public GetChatByFriendIdQueryHandler(IDbContext context, IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<GetChatByFriendIdResponse> Handle(GetChatByFriendIdQuery request, CancellationToken cancellationToken)
        {
            var chat = await _context.Chats
                .Include(x => x.Messages)
                .FirstOrDefaultAsync(x => x.User1Id == _userContext.GetUserId() && x.User2Id == request.FriendId,
                    cancellationToken: cancellationToken)
                       ?? throw new EntityNotFoundException<Chat>("Chat not found");

            return new GetChatByFriendIdResponse
            {
                Chat = new GetAllChatsResponseItem
                {
                    LastMessage = new GetAllChatsMessageResponseItem
                    {
                        ChatId = chat.Id,
                        Content = chat.LastMessage.Content,
                        IsRead = chat.LastMessage.IsRead,
                        SenderId = chat.LastMessage.SenderId,
                        Timestamp = chat.LastMessage.Timestamp
                    },
                    User1Id = chat.User1Id,
                    User2Id = chat.User2Id,
                },
                Messages = chat.Messages.Select(x => new GetAllChatsMessageResponseItem
                {
                    ChatId = chat.Id,
                    Content = x.Content,
                    IsRead = x.IsRead,
                    SenderId = x.SenderId,
                    Timestamp = x.Timestamp
                }).ToList()
            };
        }
    }
}