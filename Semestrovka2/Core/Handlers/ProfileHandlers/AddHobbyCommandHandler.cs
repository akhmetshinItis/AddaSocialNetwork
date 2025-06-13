using Core.Abstractions;
using Core.Entities;
using MediatR;
using Contracts.Responses.HobbyResponses;
using Core.Requests.ProfileRequests;

namespace Core.Handlers.ProfileHandlers;

public class AddHobbyCommandHandler : IRequestHandler<AddHobbyCommand, AddHobbyResponse>
{
    private readonly IDbContext _context;
    private readonly IS3Service _s3Service;
    private readonly IUserContext _userContext;
    public AddHobbyCommandHandler(IDbContext context, IS3Service s3Service, IUserContext userContext)
    {
        _context = context;
        _s3Service = s3Service;
        _userContext = userContext;
    }

    public async Task<AddHobbyResponse> Handle(AddHobbyCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();
        var hobby = new Hobby
        {
            UserId = userId,
            Name = request.Name,
            CreatedDate = DateTime.UtcNow,
        };
        _context.Hobbies.Add(hobby);
        await _context.SaveChangesAsync(cancellationToken);

        if (request.Photos != null && request.Photos.Count > 0)
        {
            foreach (var file in request.Photos)
            {
                if (file != null && file.Length > 0)
                {
                    var url = await _s3Service.UploadAsync(file, cancellationToken);
                    var photo = new HobbyPhoto
                    {
                        HobbyId = hobby.Id,
                        Path = url,
                        CreatedDate = DateTime.UtcNow
                    };
                    _context.HobbyPhotos.Add(photo);
                }
            }
            await _context.SaveChangesAsync(cancellationToken);
        }
        return new AddHobbyResponse { Succeeded = true, HobbyId = hobby.Id };
    }
} 