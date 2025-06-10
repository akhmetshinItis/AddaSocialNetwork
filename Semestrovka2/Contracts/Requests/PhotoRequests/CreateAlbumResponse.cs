using System;
using System.Collections.Generic;

namespace Core.Requests.PhotoRequests
{
    public class CreateAlbumResponse
    {
        public Guid AlbumId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<string> PhotoPaths { get; set; } = new();
    }
} 