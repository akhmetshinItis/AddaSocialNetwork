using Contracts.Requests.AdminRequests.CommentRequests;
using Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.AdminRequests.CommentRequests
{
    public class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, GetAllCommentsResponse>
    {
        private readonly IDbContext _context;

        public GetAllCommentsQueryHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<GetAllCommentsResponse> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
            => new GetAllCommentsResponse
            {
                Comments = await _context.Comments
                    .Select(x => new GetAllCommentsResponseCommentItem
                    {
                        Id = x.Id,
                        Content = x.Content,
                        UserId = x.UserId,
                        UserName = x.User.FirstName + " " + x.User.LastName,
                        PostId = x.PostId,
                        PostContent = x.Post.Content ?? "Нет контента",
                        CreatedDate = x.CreatedDate
                    })
                    .ToListAsync(cancellationToken)
            };
    }
} 