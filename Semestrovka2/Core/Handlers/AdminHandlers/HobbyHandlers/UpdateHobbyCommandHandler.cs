using Core.Abstractions;
using Core.Entities;
using MediatR;
using Contracts.Responses.HobbyResponses;
using Core.Requests.AdminRequests.HobbyRequests;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Core.Handlers.AdminHandlers.HobbyHandlers;

public class UpdateHobbyCommandHandler : IRequestHandler<UpdateHobbyCommand, UpdateHobbyResponse>
{
    private readonly IDbContext _context;
    private readonly IS3Service _s3Service;
    public UpdateHobbyCommandHandler(IDbContext context, IS3Service s3Service)
    {
        _context = context;
        _s3Service = s3Service;
    }

    public async Task<UpdateHobbyResponse> Handle(UpdateHobbyCommand request, CancellationToken cancellationToken)
    {
        var hobby = await _context.Hobbies.FirstOrDefaultAsync(h => h.Id == request.HobbyId, cancellationToken);
        if (hobby == null)
            return new UpdateHobbyResponse { Succeeded = false, Message = "Hobby not found" };
        hobby.UserId = request.UserId;
        hobby.Name = request.Name;

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
        }
        await _context.SaveChangesAsync(cancellationToken);
        return new UpdateHobbyResponse { Succeeded = true };
    }
} 