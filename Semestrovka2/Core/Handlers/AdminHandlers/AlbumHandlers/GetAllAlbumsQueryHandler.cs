using Contracts.Requests.AdminRequests.AlbumRequests;
using Core.Abstractions;
using Core.Requests.AdminRequests.AlbumRequests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Handlers.AdminHandlers.AlbumHandlers;

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
                .Select(x => new GetAllAlbumsResponseAlbumItem()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UserId = x.UserId,
                    CreatedDate = x.CreatedDate
                }).ToListAsync(cancellationToken)
        };
} 