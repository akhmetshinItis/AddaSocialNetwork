using MediatR;

namespace Core.Requests.HomePageRequests
{
    public class LikePostRequest : IRequest
    {
        public Guid PostId { get; set; }
    }
} 