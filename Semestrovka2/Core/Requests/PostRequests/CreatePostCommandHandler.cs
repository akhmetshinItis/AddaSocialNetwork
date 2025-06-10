using Core.Abstractions;
using Core.Entities;
using MediatR;

namespace Core.Requests.PostRequests
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand>
    {
        private readonly IDbContext _dbContext;
        private readonly IS3Service _s3Service;
        private readonly IUserContext _userContext;

        public CreatePostCommandHandler(IDbContext dbContext, IS3Service s3Service, IUserContext userContext)
        {
            _dbContext = dbContext;
            _s3Service = s3Service;
            _userContext = userContext;
        }

        public async Task Handle(CreatePostCommand request, CancellationToken cancellationToken)
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
            
            

            var post = new Post
            {
                UserId = _userContext.GetUserId(),
                Content = request.Content,
                Photo = fileUrl,
                Likes = new List<Like>(),
                Comments = new List<Comment>(),
                CreatedDate = DateTime.UtcNow,
            };
            
            _dbContext.Posts.Add(post);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}