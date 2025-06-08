using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Requests.PostRequests
{
    public class CreatePostCommand : IRequest
    {
        public string? Content { get; set; }
        public IFormFile? File { get; set; }
    }
}