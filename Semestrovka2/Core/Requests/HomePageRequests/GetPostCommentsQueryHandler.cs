using Contracts.Requests.HomePageRequests;
using Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.HomePageRequests
{
    public class GetPostCommentsQueryHandler : IRequestHandler<GetPostCommentsRequest, GetPostCommentsResponse>
    {
        private readonly IDbContext _dbContext;

        public GetPostCommentsQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetPostCommentsResponse> Handle(GetPostCommentsRequest request, CancellationToken cancellationToken)
        {
            var post = await _dbContext.Posts
                .Include(p => p.Comments)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(p => p.Id == request.PostId, cancellationToken);

            if (post == null)
            {
                throw new Exception("Post not found");
            }

            return new GetPostCommentsResponse
            {
                Comments = post.Comments.OrderByDescending(x => x.CreatedDate)
                    .Select(c => new CommentResponseItem
                    {
                        Content = c.Content,
                        UserPhoto = c.User!.ImageUrl!,
                        UserName = c.User.FirstName + " " + c.User.LastName
                    }).ToList()
            };
        }
    }
} 