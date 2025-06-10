using Core.Abstractions;
using Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.HomePageRequests
{
    public class LikePostCommandHandler : IRequestHandler<LikePostRequest>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;

        public LikePostCommandHandler(IDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task Handle(LikePostRequest request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetUserId();
            var post = await _dbContext.Posts
                .Include(p => p.Likes)
                .FirstOrDefaultAsync(p => p.Id == request.PostId, cancellationToken);

            if (post == null)
            {
                throw new Exception("Post not found");
            }

            var existingLike = post.Likes.FirstOrDefault(l => l.UserId == userId);

            if (existingLike != null)
            {
                // Unlike
                post.Likes.Remove(existingLike);
                _dbContext.Likes.Remove(existingLike);
            }
            else
            {
                // Like
                var like = new Like
                {
                    UserId = userId,
                    PostId = post.Id,
                    CreatedDate = DateTime.UtcNow
                };
                post.Likes.Add(like);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
} 