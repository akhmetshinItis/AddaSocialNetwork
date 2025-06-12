using Contracts.Requests.AdminRequests.ProfileRequests;
using MediatR;

namespace Core.Requests.AdminRequests.ProfileRequests
{
    public class GetAllProfilesQuery : IRequest<GetAllProfilesResponse>
    {
    }
} 