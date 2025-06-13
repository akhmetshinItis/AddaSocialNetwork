using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using MediatR;

namespace Core.Requests.AdminRequests.PhotoRequests
{
    public class CreatePhotoCommandHandler : IRequestHandler<CreatePhotoCommand>
    {
        private readonly IDbContext _dbContext;
        private readonly IS3Service _s3Service;

        public CreatePhotoCommandHandler(IDbContext dbContext, IS3Service s3Service)
        {
            _dbContext = dbContext;
            _s3Service = s3Service;
        }

        public async Task Handle(CreatePhotoCommand request, CancellationToken cancellationToken)
        {
            var file = request.File;
            string fileUrl = null;

            if (file != null)
            {
                const long maxFileSize = 5 * 1024 * 1024;
                if (file.Length > maxFileSize)
                    throw new ArgumentException("Файл слишком большой. Максимальный размер — 5 МБ.");

                var allowedContentTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                if (!allowedContentTypes.Contains(file.ContentType))
                    throw new ArgumentException("Недопустимый тип файла. Разрешены только изображения (jpeg, png, gif).");

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                    throw new ArgumentException("Недопустимое расширение файла.");
                
                fileUrl = await _s3Service.UploadAsync(file, cancellationToken);
            }

            var photo = new Photo
            {
                AlbumId = request.AlbumId ?? throw new InvalidOperationException(),
                Path = fileUrl,
                CreatedDate = DateTime.UtcNow
            };
            
            _dbContext.Photos.Add(photo);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
} 