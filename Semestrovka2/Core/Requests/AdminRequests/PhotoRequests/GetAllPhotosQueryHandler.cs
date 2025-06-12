using Contracts.Requests.AdminRequests.PhotoRequests;
using Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.AdminRequests.PhotoRequests
{
    public class GetAllPhotosQueryHandler : IRequestHandler<GetAllPhotosQuery, GetAllPhotosResponse>
    {
        private readonly IDbContext _context;

        public GetAllPhotosQueryHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<GetAllPhotosResponse> Handle(GetAllPhotosQuery request, CancellationToken cancellationToken)
            => new GetAllPhotosResponse
            {
                Photos = await _context.Photos
                    .Select(x => new GetAllPhotosResponsePhotoItem
                    {
                        Id = x.Id,
                        UserId = x.Album.UserId,
                        AlbumId = x.AlbumId,
                        AlbumName = x.Album.Name,
                        Path = x.Path,
                        CreatedDate = x.CreatedDate
                    })
                    .ToListAsync(cancellationToken)
            };
    }
} 