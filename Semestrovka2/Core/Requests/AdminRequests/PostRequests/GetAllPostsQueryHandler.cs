using Contracts.Requests.AdminRequests.PostRequests;
using Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.AdminRequests.PostRequests
{
    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, GetAllPostsResponse>
    {
        private readonly IDbContext _context;

        public GetAllPostsQueryHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<GetAllPostsResponse> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
            => new GetAllPostsResponse
            {
                Posts = await _context.Posts
                    .Select(x => new GetAllPostsResponsePostItem
                    {
                        Id = x.Id,
                        Content = x.Content,
                        Photo = x.Photo,
                        UserId = x.UserId,
                        UserName = x.User.FirstName + " " + x.User.LastName,
                        LikesCount = x.Likes.Count,
                        CommentsCount = x.Comments.Count,
                        CreatedDate = x.CreatedDate
                    })
                    .ToListAsync(cancellationToken)
            };
    }
} 