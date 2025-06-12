using Contracts.Requests.AdminRequests.MessageRequests;
using MediatR;

namespace Core.Requests.AdminRequests.MessageRequests
{
    public class GetAllMessagesQuery : IRequest<GetAllMessagesResponse>
    {
    }
} 