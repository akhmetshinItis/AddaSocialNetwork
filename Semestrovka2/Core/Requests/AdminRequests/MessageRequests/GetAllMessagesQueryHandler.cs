using Contracts.Requests.AdminRequests.MessageRequests;
using Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.AdminRequests.MessageRequests
{
    public class GetAllMessagesQueryHandler : IRequestHandler<GetAllMessagesQuery, GetAllMessagesResponse>
    {
        private readonly IDbContext _context;

        public GetAllMessagesQueryHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<GetAllMessagesResponse> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
            => new GetAllMessagesResponse
            {
                Messages = await _context.Messages
                    .Include(x => x.Chat)
                    .Select(x => new GetAllMessagesResponseMessageItem
                    {
                        Id = x.Id,
                        ChatId = x.ChatId,
                        SenderId = x.SenderId,
                        SenderName = x.Sender != null ? x.Sender.FirstName + " " + x.Sender.LastName : "Unknown",
                        Content = x.Content,
                        Timestamp = x.Timestamp,
                        IsRead = x.IsRead,
                        CreatedDate = x.CreatedDate
                    })
                    .ToListAsync(cancellationToken)
            };
    }
}