using Core.Abstractions;
using Core.Entities;
using Core.Requests.AdminRequests.AlbumRequests;
using MediatR;

namespace Core.Handlers.AdminHandlers.AlbumHandlers;

public class CreateAlbumCommandHandler : IRequestHandler<CreateAlbumCommand, Contracts.Responses.AlbumResponses.CreateAlbumResponse>
{
    private readonly IDbContext _context;

    public CreateAlbumCommandHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<Contracts.Responses.AlbumResponses.CreateAlbumResponse> Handle(CreateAlbumCommand request, CancellationToken cancellationToken)
    {
        var album = new Album
        {
            Name = request.Name,
            UserId = request.UserId,
            CreatedDate = DateTime.UtcNow
        };

        await _context.Albums.AddAsync(album, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new Contracts.Responses.AlbumResponses.CreateAlbumResponse
        {
            Succeeded = true,
            Message = "Album created successfully"
        };
    }
} 