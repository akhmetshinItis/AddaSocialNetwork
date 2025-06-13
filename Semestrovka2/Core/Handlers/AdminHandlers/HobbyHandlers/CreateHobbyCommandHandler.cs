using Core.Abstractions;
using Core.Entities;
using MediatR;
using Contracts.Responses.HobbyResponses;
using Core.Requests.AdminRequests.HobbyRequests;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Core.Handlers.AdminHandlers.HobbyHandlers;

public class CreateHobbyCommandHandler : IRequestHandler<CreateHobbyCommand, CreateHobbyResponse>
{
    private readonly IDbContext _context;
    private readonly IS3Service _s3Service;
    public CreateHobbyCommandHandler(IDbContext context, IS3Service s3Service)
    {
        _context = context;
        _s3Service = s3Service;
    }

    public async Task<CreateHobbyResponse> Handle(CreateHobbyCommand request, CancellationToken cancellationToken)
    {
        var hobby = new Hobby
        {
            UserId = request.UserId,
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
        return new CreateHobbyResponse { Succeeded = true, HobbyId = hobby.Id };
    }
} 