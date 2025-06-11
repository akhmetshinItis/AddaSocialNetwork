using Contracts.Requests.ProfileRequests;
using Core.Abstractions;
using Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.ProfileRequests.UpdateProfile
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileRequest>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;

        public UpdateProfileCommandHandler(IDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task Handle(UpdateProfileRequest request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetUserId();
            var profile = await _dbContext.ProfileDatas
                .FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);

            if (profile == null)
            {
                profile = new ProfileData
                {
                    UserId = userId
                };
                _dbContext.ProfileDatas.Add(profile);
            }

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