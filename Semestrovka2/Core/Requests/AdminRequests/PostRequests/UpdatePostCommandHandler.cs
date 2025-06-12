using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.AdminRequests.PostRequests
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand>
    {
        private readonly IDbContext _dbContext;
        private readonly IS3Service _s3Service;

        public UpdatePostCommandHandler(IDbContext dbContext, IS3Service s3Service)
        {
            _dbContext = dbContext;
            _s3Service = s3Service;
        }

        public async Task Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _dbContext.Posts
                .FirstOrDefaultAsync(x => x.Id == request.PostId, cancellationToken);

            if (post == null)
                throw new Exception("Пост не найден");

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

                // // Удаляем старое фото, если оно есть
                // if (!string.IsNullOrEmpty(post.Photo))
                // {
                //     await _s3Service.DeleteAsync(post.Photo, cancellationToken);
                // }

                // Загружаем новое фото
                post.Photo = await _s3Service.UploadAsync(request.File, cancellationToken);
            }

            post.Content = request.Content;
            post.UserId = request.UserId;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
} 