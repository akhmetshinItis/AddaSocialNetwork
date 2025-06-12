using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Requests.AdminRequests.PostRequests
{
    public class UpdatePostCommand : IRequest
    {
        public Guid PostId { get; set; }
        public string? Content { get; set; }
        public IFormFile? File { get; set; }
        public Guid UserId { get; set; }
    }
} 