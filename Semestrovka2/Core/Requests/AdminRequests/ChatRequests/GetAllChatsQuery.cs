using Contracts.Requests.AdminRequests.ChatRequests;
using MediatR;

namespace Core.Requests.AdminRequests.ChatRequests
{
    public class GetAllChatsQuery : IRequest<GetAllChatsResponse>
    {
    }
} 