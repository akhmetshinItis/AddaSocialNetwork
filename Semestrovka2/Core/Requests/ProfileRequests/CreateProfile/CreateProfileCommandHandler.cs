using Core.Abstractions;
using Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.ProfileRequests.CreateProfile
{
    public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand>
    {
        private readonly IDbContext _dbContext;

        public CreateProfileCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            var existingProfile = await _dbContext.ProfileDatas
                .FirstOrDefaultAsync(p => p.UserId == request.UserId, cancellationToken);

            if (existingProfile != null)
            {
                // Если профиль существует, но помечен как удаленный, восстанавливаем его
                if (existingProfile.IsDeleted)
                {
                    existingProfile.IsDeleted = false;
                    await _dbContext.SaveChangesAsync(cancellationToken);
                }
                return;
            }

            // Создаем новый профиль
            var profile = new ProfileData
            {
                UserId = request.UserId,
                BackgroundImage = "/assets/images/banner/profile-banner.jpg",
                AboutMe = string.Empty,
                WorkingZone = string.Empty,
                Education = string.Empty,
                Contacts = string.Empty,
                FacebookLink = string.Empty,
                TwitterLink = string.Empty,
                GoogleLink = string.Empty,
                PinterestLink = string.Empty
            };

            _dbContext.ProfileDatas.Add(profile);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
} 