using Core.Abstractions;
using Core.Requests.AdminRequests.FriendRequests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Handlers.AdminHandlers.FriendHandlers;

public class DeleteFriendCommandHandler : IRequestHandler<DeleteFriendCommand, Contracts.Responses.FriendResponses.DeleteFriendResponse>
{
    private readonly IDbContext _context;

    public DeleteFriendCommandHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<Contracts.Responses.FriendResponses.DeleteFriendResponse> Handle(DeleteFriendCommand request, CancellationToken cancellationToken)
    {
        var friend = await _context.Friends
            .FirstOrDefaultAsync(f => f.Id == request.FriendId, cancellationToken);
        
        if (friend == null)
        {
            return new Contracts.Responses.FriendResponses.DeleteFriendResponse
            {
                Succeeded = false,
                Message = "Friend not found"
            };
        }

        friend.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);

        return new Contracts.Responses.FriendResponses.DeleteFriendResponse
        {
            Succeeded = true,
            Message = "Friend deleted successfully"
        };
    }
} 