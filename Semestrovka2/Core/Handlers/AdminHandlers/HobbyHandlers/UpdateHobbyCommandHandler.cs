using Core.Abstractions;
using Core.Entities;
using MediatR;
using Contracts.Responses.HobbyResponses;
using Core.Requests.AdminRequests.HobbyRequests;
using Microsoft.EntityFrameworkCore;

namespace Core.Handlers.AdminHandlers.HobbyHandlers;

public class UpdateHobbyCommandHandler : IRequestHandler<UpdateHobbyCommand, UpdateHobbyResponse>
{
    private readonly IDbContext _context;
    public UpdateHobbyCommandHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateHobbyResponse> Handle(UpdateHobbyCommand request, CancellationToken cancellationToken)
    {
        var hobby = await _context.Hobbies.FirstOrDefaultAsync(h => h.Id == request.HobbyId, cancellationToken);
        if (hobby == null)
            return new UpdateHobbyResponse { Succeeded = false, Message = "Hobby not found" };
        hobby.UserId = request.UserId;
        hobby.Name = request.Name;
        await _context.SaveChangesAsync(cancellationToken);
        return new UpdateHobbyResponse { Succeeded = true };
    }
} 