using Contracts.Requests.AdminRequests.ProfileRequests;
using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.AdminRequests.ProfileRequests
{
    public class DeleteProfileCommandHandler : IRequestHandler<DeleteProfileCommand, DeleteProfileResponse>
    {
        private readonly IDbContext _dbContext;

        public DeleteProfileCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DeleteProfileResponse> Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = await _dbContext.ProfileDatas
                .FirstOrDefaultAsync(p => p.Id == request.ProfileId, cancellationToken)
                ?? throw new EntityNotFoundException<ProfileData>("Профиль не найден");

            profile.IsDeleted = true;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteProfileResponse
            {
                Succeeded = true
            };
        }
    }
} 