using Contracts.Requests.AdminRequests.MessagesRequest;
using MediatR;

namespace Core.Requests.AdminRequests.MessageRequests
{
    public class GetAllMessagesQuery : IRequest<GetAllMessagesResponse>
    {
    }
} 