using Contracts.Requests.ProfileRequests;
using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.ProfileRequests.GetProfilePhotos
{
    public class GetProfilePhotosQueryHandler : IRequestHandler<GetProfilePhotosQuery, GetProfilePhotosResponse>
    {
        private readonly IDbContext _context;
        private readonly IUserContext _userContext;

        public GetProfilePhotosQueryHandler(IDbContext context, IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<GetProfilePhotosResponse> Handle(GetProfilePhotosQuery request, CancellationToken cancellationToken)
        {
            Guid userId = _userContext.GetUserId();
            if(request.UserId.HasValue)
                userId = request.UserId.Value;
            
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken: cancellationToken)
                ?? throw new EntityNotFoundException<User>("Пользователь не найден");

            var profile = await _context.ProfileDatas.FirstOrDefaultAsync(p => p.UserId == userId,
                cancellationToken: cancellationToken) 
                ?? throw new EntityNotFoundException<ProfileData>("профиль не найден");

            return new GetProfilePhotosResponse
            {
                ProfileImage = user.ImageUrl,
                BackgroundImage = profile.BackgroundImage,
            };
        }
    }
}