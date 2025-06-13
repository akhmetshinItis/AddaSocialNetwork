using Core.Abstractions;
using Core.Entities;
using MediatR;
using Contracts.Responses.HobbyResponses;
using Core.Requests.AdminRequests.HobbyRequests;

namespace Core.Handlers.AdminHandlers.HobbyHandlers;

public class CreateHobbyCommandHandler : IRequestHandler<CreateHobbyCommand, CreateHobbyResponse>
{
    private readonly IDbContext _context;
    public CreateHobbyCommandHandler(IDbContext context)
    {
        _context = context;
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
        return new CreateHobbyResponse { Succeeded = true, HobbyId = hobby.Id };
    }
} 