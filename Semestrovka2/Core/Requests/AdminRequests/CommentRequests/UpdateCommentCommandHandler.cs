using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.AdminRequests.CommentRequests
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand>
    {
        private readonly IDbContext _dbContext;

        public UpdateCommentCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _dbContext.Comments
                .FirstOrDefaultAsync(x => x.Id == request.CommentId, cancellationToken);

            if (comment == null)
                throw new Exception("Комментарий не найден");

            comment.Content = request.Content;
            comment.UserId = request.UserId;
            comment.PostId = request.PostId;
            comment.UpdatedDate = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
} 