using Contracts.Requests.AdminRequests.AlbumRequests;
using Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.AdminRequests.AlbumRequests
{
    public class GetAllAlbumsQueryHandler : IRequestHandler<GetAllAlbumsQuery, GetAllAlbumsResponse>
    {
        private readonly IDbContext _context;

        public GetAllAlbumsQueryHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<GetAllAlbumsResponse> Handle(GetAllAlbumsQuery request, CancellationToken cancellationToken)
            => new GetAllAlbumsResponse
            {
                Albums = await _context.Albums
                    .Select(x => new GetAllAlbumsResponseAlbumItem
                    {
                        Id = x.Id,
                        UserId = x.UserId,
                        Name = x.Name,
                        CreatedDate = x.CreatedDate,
                        PhotoCount = x.Photos!.Count,
                    })
                    .ToListAsync(cancellationToken)
            };
    }
} 