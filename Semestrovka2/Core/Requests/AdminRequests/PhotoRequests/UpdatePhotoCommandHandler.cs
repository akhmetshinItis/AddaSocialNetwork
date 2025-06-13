using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.AdminRequests.PhotoRequests
{
    public class UpdatePhotoCommandHandler : IRequestHandler<UpdatePhotoCommand>
    {
        private readonly IDbContext _dbContext;
        private readonly IS3Service _s3Service;

        public UpdatePhotoCommandHandler(IDbContext dbContext, IS3Service s3Service)
        {
            _dbContext = dbContext;
            _s3Service = s3Service;
        }

        public async Task Handle(UpdatePhotoCommand request, CancellationToken cancellationToken)
        {
            var photo = await _dbContext.Photos
                .FirstOrDefaultAsync(x => x.Id == request.PhotoId, cancellationToken);

            if (photo == null)
                throw new Exception("Фото не найдено");

            if (request.File != null)
            {
                const long maxFileSize = 5 * 1024 * 1024;
                if (request.File.Length > maxFileSize)
                    throw new ArgumentException("Файл слишком большой. Максимальный размер — 5 МБ.");

                var allowedContentTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                if (!allowedContentTypes.Contains(request.File.ContentType))
                    throw new ArgumentException("Недопустимый тип файла. Разрешены только изображения (jpeg, png, gif).");

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(request.File.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                    throw new ArgumentException("Недопустимое расширение файла.");

                photo.Path = await _s3Service.UploadAsync(request.File, cancellationToken);
            }

            photo.AlbumId = request.AlbumId ?? throw new ArgumentNullException(nameof(request.AlbumId));
            photo.UpdatedDate = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
} 