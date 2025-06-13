using Core.Abstractions;
using Core.Requests.AdminRequests.FriendRequests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Handlers.AdminHandlers.FriendHandlers;

public class UpdateFriendCommandHandler : IRequestHandler<UpdateFriendCommand, Contracts.Responses.FriendResponses.UpdateFriendResponse>
{
    private readonly IDbContext _context;

    public UpdateFriendCommandHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<Contracts.Responses.FriendResponses.UpdateFriendResponse> Handle(UpdateFriendCommand request, CancellationToken cancellationToken)
    {
        var friend = await _context.Friends
            .FirstOrDefaultAsync(f => f.Id == request.FriendId, cancellationToken);
        
        if (friend == null)
        {
            return new Contracts.Responses.FriendResponses.UpdateFriendResponse
            {
                Succeeded = false,
                Message = "Friend not found"
            };
        }

        friend.User1 = request.User1;
        friend.User2 = request.User2;

        await _context.SaveChangesAsync(cancellationToken);

        return new Contracts.Responses.FriendResponses.UpdateFriendResponse
        {
            Succeeded = true,
            Message = "Friend updated successfully"
        };
    }
} 