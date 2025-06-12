using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.AdminRequests.ProfileRequests
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand>
    {
        private readonly IDbContext _dbContext;

        public UpdateProfileCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = await _dbContext.ProfileDatas
                .FirstOrDefaultAsync(p => p.Id == request.ProfileId, cancellationToken)
                ?? throw new EntityNotFoundException<ProfileData>("Профиль не найден");

            profile.AboutMe = request.AboutMe;
            profile.WorkingZone = request.WorkingZone;
            profile.Education = request.Education;
            profile.Contacts = request.Contacts;
            profile.FacebookLink = request.FacebookLink;
            profile.TwitterLink = request.TwitterLink;
            profile.GoogleLink = request.GoogleLink;
            profile.PinterestLink = request.PinterestLink;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
} 