using Contracts.Requests.AdminRequests.MessagesRequest;
using Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.AdminRequests.MessageRequests
{
    public class GetAllMessagesQueryHandler : IRequestHandler<GetAllMessagesQuery, GetAllMessagesResponse>
    {
        private readonly IDbContext _context;
        public async Task<GetAllMessagesResponse> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
        {
            return new GetAllMessagesResponse
            {
                Messages = await _context.Messages.Select(x => new GetAllMessagesResponseItem
                {
                    ChatId = x.ChatId,
                    Content = x.Content,
                    IsRead = x.IsRead,
                    SenderId = x.SenderId,
                    Timestamp = x.Timestamp
                }).ToListAsync(cancellationToken: cancellationToken)
            };
        }
    }
}