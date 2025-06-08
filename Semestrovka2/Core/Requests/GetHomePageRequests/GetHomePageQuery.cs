using Contracts.Requests.HomePageRequests;
using MediatR;

namespace Core.Requests.GetHomePageRequests
{
    public class GetHomePageQuery : IRequest<GetHomePageResponse>
    {
    }
}