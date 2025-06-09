using Contracts.Requests.FriendsRequests.GetFriendsList;
using Contracts.Requests.HomePageRequests;
using Core.Abstractions;
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
            IFriendsService friendsService)
        {
            _dbContext = dbContext;
            _businessUserService = businessUserService;
            _friendsService = friendsService;
        }

        public async Task<GetHomePageResponse> Handle(GetHomePageQuery request, CancellationToken cancellationToken)
        {
            // говнокод еще тот, но фиксить уже нет желания, перегорел
            var user = _businessUserService.GetCurrentUser();
            var profile = _dbContext.ProfileDatas.FirstOrDefault(x => x.UserId == user.Id);
            var friendIds = _friendsService.GetFriends(user.Id).Select(x => x.Id);
            var posts = _dbContext.Posts
                .Include(x => x.User)
                .Where(x => friendIds.Contains(x.UserId) || x.UserId == user.Id)
                .OrderByDescending(x => x.CreatedDate);

            var response = new GetHomePageResponse
            {
                Posts = await posts.Select(x => new PostResponseItem
                {
                    UserId = x.UserId,
                    UserPhoto = x.User!.ImageUrl!,
                    Content = x.Content,
                    Photo = x.Photo,
                    Likes = x.Likes.Count,
                    UserName = x.User.FirstName + " " + x.User.LastName,
                    Time = FormatPostTimeAgo(x.CreatedDate),
                    Comments = x.Comments.Select(x => new CommentResponseItem
                    {
                        UserId = x.UserId,
                        Content = x.Content,
                        UserPhoto = x.User!.ImageUrl!,
                    }).ToList(),
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