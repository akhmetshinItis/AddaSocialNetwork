using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Requests.AdminRequests.PhotoRequests
{
    public class UpdatePhotoCommand : IRequest
    {
        public Guid PhotoId { get; set; }
        public IFormFile? File { get; set; }
        public Guid UserId { get; set; }
        public Guid? AlbumId { get; set; }
    }
} 