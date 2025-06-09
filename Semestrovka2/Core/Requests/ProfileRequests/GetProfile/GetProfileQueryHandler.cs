using Contracts.Requests.ProfileRequests;
using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using MediatR;

namespace Core.Requests.ProfileRequests.GetProfile
{
    public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, GetProfileResponse>
    {
        private readonly IUserContext _userContext;
        private readonly IDbContext _dbContext;

        public GetProfileQueryHandler(IUserContext userContext, IDbContext dbContext)
        {
            _userContext = userContext;
            _dbContext = dbContext;
        }

        public Task<GetProfileResponse> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            Guid userId = _userContext.GetUserId() ?? throw new UnauthorizedAccessException();
            if (!request.IsCurrentUserProfile && request.UserId.HasValue)
            {
                userId = request.UserId.Value;
            }
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId)
                ?? throw new EntityNotFoundException<User>("пользователь не найден");
            var profile = _dbContext.ProfileDatas.FirstOrDefault(p => p.UserId == userId);

            var response = new GetProfileResponse
            {
                ProfileImage = user.ImageUrl,
                BackgroundImage = profile.BackgroundImage,
                UserName = user.FirstName + " " + user.LastName,
                AboutMe = profile.AboutMe,
                WorkingZone = profile.WorkingZone,
                Education = profile.Education,
                Contacts = profile.Contacts,
                FacebookLink = profile.FacebookLink,
                TwitterLink = profile.TwitterLink,
                GoogleLink = profile.GoogleLink,
                PinterestLink = profile.PinterestLink,
                IsCurrentUserProfile = request.IsCurrentUserProfile,
                UserId = userId,
            };
            
            return Task.FromResult(response);
        }
    }
}