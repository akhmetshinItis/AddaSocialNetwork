using Contracts.Requests.PhotoRequests;
using Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.PhotoRequests
{
    public class GetAlbumsQueryHandler : IRequestHandler<GetAlbumsQuery, GetAlbumsResponse>
    {
        private readonly IUserContext _userContext;
        private readonly IDbContext _dbContext;

        public GetAlbumsQueryHandler(IUserContext userContext, IDbContext dbContext)
        {
            _userContext = userContext;
            _dbContext = dbContext;
        }

        public async Task<GetAlbumsResponse> Handle(GetAlbumsQuery request, CancellationToken cancellationToken)
        {
            Guid userId;
            if (request.UserId.HasValue)
                userId = request.UserId.Value;
            else
                userId = _userContext.GetUserId();

            var albums = _dbContext.Albums.Where(x => x.UserId == userId);
            
            if(request.SortByAmount.HasValue)
                albums = albums.OrderBy(x => x.Photos!.Count);
            
            if (request.SortByDate.HasValue)
                albums = albums.OrderBy(x => x.CreatedDate);
            
            return new GetAlbumsResponse
            {
                Albums = await albums.Select(x => new AlbumResponseItem
                {
                    Name = x.Name,
                    Photos = x.Photos.Select(x => new PhotoResponseItem
                    {
                        Path = x.Path,
                    }).ToList()
                }).ToListAsync(cancellationToken: cancellationToken),
                UserId = userId,
                IsCurrentUserProfile = userId == _userContext.GetUserId()
            };
        }
    }
}