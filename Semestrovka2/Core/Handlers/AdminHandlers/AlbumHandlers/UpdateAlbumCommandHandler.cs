using Core.Abstractions;
using Core.Requests.AdminRequests.AlbumRequests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Handlers.AdminHandlers.AlbumHandlers;

public class UpdateAlbumCommandHandler : IRequestHandler<UpdateAlbumCommand, Contracts.Responses.AlbumResponses.UpdateAlbumResponse>
{
    private readonly IDbContext _context;

    public UpdateAlbumCommandHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<Contracts.Responses.AlbumResponses.UpdateAlbumResponse> Handle(UpdateAlbumCommand request, CancellationToken cancellationToken)
    {
        var album = await _context.Albums
            .FirstOrDefaultAsync(a => a.Id == request.AlbumId && !a.IsDeleted, cancellationToken);
        
        if (album == null)
        {
            return new Contracts.Responses.AlbumResponses.UpdateAlbumResponse
            {
                Succeeded = false,
                Message = "Album not found"
            };
        }

        album.Name = request.Name;
        album.UserId = request.UserId;

        await _context.SaveChangesAsync(cancellationToken);

        return new Contracts.Responses.AlbumResponses.UpdateAlbumResponse
        {
            Succeeded = true,
            Message = "Album updated successfully"
        };
    }
} 