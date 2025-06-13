using Core.Abstractions;
using Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.AdminRequests.CommentRequests
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
    {
        private readonly IDbContext _dbContext;

        public DeleteCommentCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _dbContext.Comments
                .FirstOrDefaultAsync(x => x.Id == request.CommentId, cancellationToken);

            if (comment == null)
                throw new Exception("Комментарий не найден");

            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
} 