using Contracts.Requests.HomePageRequests;
using Core.Abstractions;
using Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.HomePageRequests
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentRequest>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;

        public CreateCommentCommandHandler(IDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task Handle(CreateCommentRequest request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetUserId();
            var post = await _dbContext.Posts
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == request.PostId, cancellationToken);

            if (post == null)
            {
                throw new Exception("Post not found");
            }

            var comment = new Comment
            {
                UserId = userId,
                PostId = post.Id,
                Content = request.Content,
                CreatedDate = DateTime.UtcNow
            };

            post.Comments.Add(comment);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
} 