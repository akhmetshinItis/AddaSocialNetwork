using Core.Abstractions;
using Core.Entities;
using MediatR;

namespace Core.Requests.PhotoRequests
{
    public class CreateAlbumCommandHandler : IRequestHandler<CreateAlbumCommand, CreateAlbumResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IS3Service _s3Service;

        public CreateAlbumCommandHandler(IDbContext dbContext, IS3Service s3Service)
        {
            _dbContext = dbContext;
            _s3Service = s3Service;
        }

        public async Task<CreateAlbumResponse> Handle(CreateAlbumCommand request, CancellationToken cancellationToken)
        {
            var paths = new List<string>();

            foreach (var photo in request.Photos)
            {
               var path = await _s3Service.UploadAsync(photo);
               paths.Add(path);
            }

            if (paths.Count == 0)
                throw new InvalidOperationException("фото не добавлены");
            
            var album = new Album
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                UserId = request.UserId,
                CreatedDate = DateTime.UtcNow,
                Photos = paths.Select(x => new Photo
                {
                    CreatedDate = DateTime.UtcNow,
                    Id = Guid.NewGuid(),
                    Path = x,
                }).ToList()
            };
            
            _dbContext.Albums.Add(album);
            await _dbContext.SaveChangesAsync();

            return new CreateAlbumResponse
            {
                AlbumId = album.Id,
                Name = album.Name,
                PhotoPaths = album.Photos.Select(x => x.Path).ToList(),
            };
        }
    }
}