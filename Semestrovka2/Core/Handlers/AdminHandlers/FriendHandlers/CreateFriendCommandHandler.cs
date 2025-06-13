using Core.Abstractions;
using Core.Entities;
using Core.Requests.AdminRequests.FriendRequests;
using MediatR;

namespace Core.Handlers.AdminHandlers.FriendHandlers;

public class CreateFriendCommandHandler : IRequestHandler<CreateFriendCommand, Contracts.Responses.FriendResponses.CreateFriendResponse>
{
    private readonly IDbContext _context;

    public CreateFriendCommandHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<Contracts.Responses.FriendResponses.CreateFriendResponse> Handle(CreateFriendCommand request, CancellationToken cancellationToken)
    {
        var friend = new Friend
        {
            Id = Guid.NewGuid(),
            User1 = request.User1,
            User2 = request.User2,
            CreatedDate = DateTime.UtcNow
        };

        await _context.Friends.AddAsync(friend, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new Contracts.Responses.FriendResponses.CreateFriendResponse
        {
            Succeeded = true,
            Message = "Friend created successfully"
        };
    }
} 