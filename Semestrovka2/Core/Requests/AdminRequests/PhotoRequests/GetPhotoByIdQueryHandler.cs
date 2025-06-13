using Contracts.Requests.AdminRequests.PhotoRequests;
using Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.AdminRequests.PhotoRequests
{
    public class GetPhotoByIdQueryHandler : IRequestHandler<GetPhotoByIdQuery, GetAllPhotosResponsePhotoItem>
    {
        private readonly IDbContext _context;

        public GetPhotoByIdQueryHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<GetAllPhotosResponsePhotoItem> Handle(GetPhotoByIdQuery request, CancellationToken cancellationToken)
        {
            var photo = await _context.Photos
                .Include(x => x.Album)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (photo == null)
                return null;

            return new GetAllPhotosResponsePhotoItem
            {
                Id = photo.Id,
                Url = photo.Path,
                UserId = photo.Album.UserId,
                AlbumId = photo.AlbumId,
                AlbumName = photo.Album?.Name,
                CreatedDate = photo.CreatedDate
            };
        }
    }
} 