using Contracts.Requests.AdminRequests.PostRequests;
using Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.AdminRequests.PostRequests
{
    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, GetAllPostsResponsePostItem>
    {
        private readonly IDbContext _context;

        public GetPostByIdQueryHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<GetAllPostsResponsePostItem> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts
                .Include(x => x.User)
                .Include(x => x.Likes)
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (post == null)
                return null;

            return new GetAllPostsResponsePostItem
            {
                Id = post.Id,
                Content = post.Content,
                Photo = post.Photo,
                UserId = post.UserId,
                UserName = post.User.FirstName + " " + post.User.LastName,
                LikesCount = post.Likes.Count,
                CommentsCount = post.Comments.Count,
                CreatedDate = post.CreatedDate
            };
        }
    }
} 