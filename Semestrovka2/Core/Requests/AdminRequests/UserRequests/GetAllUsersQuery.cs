using Contracts.Requests.AdminRequests.UserRequests;
using MediatR;

namespace Core.Requests.AdminRequests.UserRequests
{
    public class GetAllUsersQuery : IRequest<GetAllUsersResponse>
    {
    }
}