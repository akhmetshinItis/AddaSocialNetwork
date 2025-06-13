using Core.Abstractions;
using Core.Entities;
using MediatR;

namespace Core.Requests.AdminRequests.CommentRequests
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand>
    {
        private readonly IDbContext _dbContext;

        public CreateCommentCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new Comment
            {
                UserId = request.UserId,
                PostId = request.PostId,
                Content = request.Content,
                CreatedDate = DateTime.UtcNow
            };
            
            _dbContext.Comments.Add(comment);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
} 