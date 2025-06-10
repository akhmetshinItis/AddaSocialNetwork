using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Core.Requests.PhotoRequests
{
    public class CreateAlbumCommand : IRequest<CreateAlbumResponse>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<IFormFile> Photos { get; set; } = new();
    }
} 