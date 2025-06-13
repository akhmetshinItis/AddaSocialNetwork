using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.AdminRequests.PhotoRequests
{
    public class DeletePhotoCommandHandler : IRequestHandler<DeletePhotoCommand>
    {
        private readonly IDbContext _dbContext;
        private readonly IS3Service _s3Service;

        public DeletePhotoCommandHandler(IDbContext dbContext, IS3Service s3Service)
        {
            _dbContext = dbContext;
            _s3Service = s3Service;
        }

        public async Task Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
        {
            var photo = await _dbContext.Photos
                .FirstOrDefaultAsync(x => x.Id == request.PhotoId, cancellationToken);

            if (photo == null)
                throw new EntityNotFoundException<Photo>("Фото не найдено");
            
            photo.IsDeleted = true;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
} 