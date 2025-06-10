using Contracts.Requests.HomePageRequests;
using MediatR;

namespace Core.Requests.HomePageRequests
{
    public class GetPostCommentsRequest : IRequest<GetPostCommentsResponse>
    {
        public Guid PostId { get; set; }
    }
} 