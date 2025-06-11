using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using Core.Requests.ProfileRequests.UpdateProfile;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.ProfileRequests.UpdateProfileImage
{
    public class UpdateProfileImageCommandHandler : IRequestHandler<UpdateProfileImageRequest>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;
        private readonly IS3Service _s3Service;

        public UpdateProfileImageCommandHandler(
            IDbContext dbContext, 
            IUserContext userContext, IS3Service s3Service)
        {
            _dbContext = dbContext;
            _userContext = userContext;
            _s3Service = s3Service;
        }

        public async Task Handle(UpdateProfileImageRequest request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetUserId();
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken)
                ?? throw new EntityNotFoundException<User>("User not found");

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

            if (request.ProfileImage != null)
            {
                var profileImagePath = await _s3Service.UploadAsync(request.ProfileImage, cancellationToken);
                user.ImageUrl = profileImagePath;
            }

            if (request.BackgroundImage != null)
            {
                var backgroundImagePath = await _s3Service.UploadAsync(request.BackgroundImage, cancellationToken);
                profile.BackgroundImage = backgroundImagePath;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
} 