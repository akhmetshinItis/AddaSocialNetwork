using Contracts.Requests.AdminRequests.CommentRequests;
using Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.AdminRequests.CommentRequests
{
    public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, GetAllCommentsResponseCommentItem>
    {
        private readonly IDbContext _context;

        public GetCommentByIdQueryHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<GetAllCommentsResponseCommentItem> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
        {
            var comment = await _context.Comments
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (comment == null)
                return null;

            return new GetAllCommentsResponseCommentItem
            {
                Id = comment.Id,
                Content = comment.Content,
                PostId = comment.PostId,
                UserId = comment.UserId,
                UserName = comment.User.FirstName + " " + comment.User.LastName,
                CreatedDate = comment.CreatedDate
            };
        }
    }
} 