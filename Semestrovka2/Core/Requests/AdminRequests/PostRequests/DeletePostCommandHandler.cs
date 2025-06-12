using Contracts.Requests.AdminRequests.PostRequests;
using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.AdminRequests.PostRequests
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, DeletePostResponse>
    {
        private readonly IDbContext _dbContext;

        public DeletePostCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DeletePostResponse> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _dbContext.Posts
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .FirstOrDefaultAsync(p => p.Id == request.PostId, cancellationToken)
                ?? throw new EntityNotFoundException<Post>("Пост не найден");

            // Помечаем пост как удаленный
            post.IsDeleted = true;

            // Помечаем все комментарии как удаленные
            foreach (var comment in post.Comments)
            {
                comment.IsDeleted = true;
            }

            // Помечаем все лайки как удаленные
            foreach (var like in post.Likes)
            {
                like.IsDeleted = true;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new DeletePostResponse
            {
                Succeeded = true
            };
        }
    }
} 