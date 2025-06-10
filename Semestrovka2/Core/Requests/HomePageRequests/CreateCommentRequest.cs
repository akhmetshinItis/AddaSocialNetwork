using Core.Entities;
using MediatR;

namespace Core.Requests.HomePageRequests
{
    public class CreateCommentRequest : IRequest
    {
        public Guid PostId { get; set; }
        public string Content { get; set; }
    }
} 