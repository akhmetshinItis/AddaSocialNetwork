using Contracts.Requests.FriendsRequests.GetFriendsList;
using Contracts.Requests.HomePageRequests;
using Core.Abstractions;
using Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.GetHomePageRequests
{
    public class GetHomePageQueryHandler : IRequestHandler<GetHomePageQuery, GetHomePageResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IBusinessUserService _businessUserService;
        private readonly IFriendsService _friendsService;
        private readonly IUserContext _userContext;

        public GetHomePageQueryHandler(IDbContext dbContext,
            IBusinessUserService businessUserService,
            IFriendsService friendsService,
            IUserContext userContext)
        {
            _dbContext = dbContext;
            _businessUserService = businessUserService;
            _friendsService = friendsService;
            _userContext = userContext;
        }

        public async Task<GetHomePageResponse> Handle(GetHomePageQuery request, CancellationToken cancellationToken)
        {
            var user = _businessUserService.GetCurrentUser();
            var profile = _dbContext.ProfileDatas.FirstOrDefault(x => x.UserId == user.Id);

            if (profile == null)
            {
                profile = new ProfileData
                {
                    UserId = user.Id,
                };
                _dbContext.ProfileDatas.Add(profile);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            
            var friendIds = _friendsService.GetFriends(user.Id).Select(x => x.Id);
            var posts = _dbContext.Posts
                .Include(x => x.User)
                .Include(x => x.Likes)
                .Where(x => friendIds.Contains(x.UserId) || x.UserId == user.Id)
                .OrderByDescending(x => x.CreatedDate);

            var response = new GetHomePageResponse
            {
                Posts = await posts.Select(x => new PostResponseItem
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    UserPhoto = x.User!.ImageUrl!,
                    Content = x.Content,
                    Photo = x.Photo,
                    Likes = x.Likes.Count,
                    IsLiked = x.Likes.Any(l => l.UserId == user.Id),
                    UserName = x.User.FirstName + " " + x.User.LastName,
                    Time = FormatPostTimeAgo(x.CreatedDate),
                    CommentsCount = x.Comments.Count,
                }).ToListAsync(cancellationToken: cancellationToken),
                HomeProfileResponseItem = new HomeProfileResponseItem
                {
                    UserPhoto = user.ImageUrl,
                    BackgroundPhoto = profile.BackgroundImage,
                    Description = profile.AboutMe,
                    Username = user.FirstName + " " + user.LastName,
                },
                FriendsList = await _friendsService.GetFriends(user.Id).Select(x
                    => new GetFriendsListUserResponseItem
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        ImageUrl = x.ImageUrl,
                    }).ToListAsync(cancellationToken: cancellationToken),
            };
            
            return response;
        }
        
        public static string FormatPostTimeAgo(DateTime? createdDate)
        {
            if (!createdDate.HasValue)
                return "";

            var timeSpan = DateTime.UtcNow - createdDate.Value;

            if (timeSpan.TotalMinutes < 1)
                return "только что";
            else if (timeSpan.TotalMinutes < 60)
                return $"{(int)timeSpan.TotalMinutes} минут назад";
            else if (timeSpan.TotalHours < 24)
                return $"{(int)timeSpan.TotalHours} часов назад";
            else if (timeSpan.TotalDays < 7)
                return $"{(int)timeSpan.TotalDays} дней назад";
            else
                return createdDate.Value.ToString("dd.MM.yyyy HH:mm");
        }
    }
}