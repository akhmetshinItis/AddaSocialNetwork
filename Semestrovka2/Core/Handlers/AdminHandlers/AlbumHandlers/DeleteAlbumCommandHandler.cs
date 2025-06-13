using Core.Abstractions;
using Core.Requests.AdminRequests.AlbumRequests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Handlers.AdminHandlers.AlbumHandlers;

public class DeleteAlbumCommandHandler : IRequestHandler<DeleteAlbumCommand, Contracts.Responses.AlbumResponses.DeleteAlbumResponse>
{
    private readonly IDbContext _context;

    public DeleteAlbumCommandHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<Contracts.Responses.AlbumResponses.DeleteAlbumResponse> Handle(DeleteAlbumCommand request, CancellationToken cancellationToken)
    {
        var album = await _context.Albums
            .Include(a => a.Photos)
            .FirstOrDefaultAsync(a => a.Id == request.AlbumId && !a.IsDeleted, cancellationToken);
        
        if (album == null)
        {
            return new Contracts.Responses.AlbumResponses.DeleteAlbumResponse
            {
                Succeeded = false,
                Message = "Album not found"
            };
        }

        // Помечаем альбом как удаленный
        album.IsDeleted = true;

        // Помечаем все фотографии альбома как удаленные
        foreach (var photo in album.Photos)
        {
            photo.IsDeleted = true;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new Contracts.Responses.AlbumResponses.DeleteAlbumResponse
        {
            Succeeded = true,
            Message = "Album deleted successfully"
        };
    }
} 