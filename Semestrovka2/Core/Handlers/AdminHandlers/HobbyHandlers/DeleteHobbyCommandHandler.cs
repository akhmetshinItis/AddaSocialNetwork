using Core.Abstractions;
using MediatR;
using Contracts.Responses.HobbyResponses;
using Core.Requests.AdminRequests.HobbyRequests;
using Microsoft.EntityFrameworkCore;

namespace Core.Handlers.AdminHandlers.HobbyHandlers;

public class DeleteHobbyCommandHandler : IRequestHandler<DeleteHobbyCommand, DeleteHobbyResponse>
{
    private readonly IDbContext _context;
    public DeleteHobbyCommandHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<DeleteHobbyResponse> Handle(DeleteHobbyCommand request, CancellationToken cancellationToken)
    {
        var hobby = await _context.Hobbies.FirstOrDefaultAsync(h => h.Id == request.HobbyId, cancellationToken);
        if (hobby == null)
            return new DeleteHobbyResponse { Succeeded = false, Message = "Hobby not found" };
        hobby.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);
        return new DeleteHobbyResponse { Succeeded = true };
    }
} 