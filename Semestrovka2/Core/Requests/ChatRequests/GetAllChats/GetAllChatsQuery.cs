using Contracts.Requests.ChatRequests.GetAllChats;
using MediatR;

namespace Core.Requests.ChatRequests.GetAllChats
{
    public class GetAllChatsQuery : IRequest<GetAllChatsResponse>
    {
    }
}