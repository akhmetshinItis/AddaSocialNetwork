using Contracts.Requests.AdminRequests.HobbyRequests;
using MediatR;

namespace Core.Requests.AdminRequests.HobbyRequests
{
    public class GetAllHobbiesQuery : IRequest<GetAllHobbiesResponse>
    {
    }
} 