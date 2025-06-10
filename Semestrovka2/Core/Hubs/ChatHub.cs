using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using Microsoft.AspNetCore.SignalR;

namespace Core.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;

        public ChatHub(IDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task JoinChat(Guid chatId)
        {
            var userId = _userContext.GetUserId(); // получи из токена или куки
            var chat = await _dbContext.Chats.FindAsync(chatId);

            if (chat == null || (chat.User1Id != userId && chat.User2Id != userId))
                throw new HubException("Access denied.");

            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        }
        
        public async Task SendMessage(Guid chatId, string message)
        {
            var cancellationToken = CancellationToken.None;
            var senderId = _userContext.GetUserId();

            var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var msg = new Message
                {
                    ChatId = chatId,
                    SenderId = senderId,
                    Content = message,
                    Timestamp = DateTime.UtcNow,
                };

                await Clients.Group(chatId.ToString())
                    .SendAsync("ReceiveMessage", message, senderId, msg.Timestamp.ToString("HH:mm"),
                        cancellationToken: cancellationToken);

                _dbContext.Messages.Add(msg);
                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                // можно добавить логирование
            }
        }
        
        // Вызывается клиентом при выходе из чата
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

    }
}