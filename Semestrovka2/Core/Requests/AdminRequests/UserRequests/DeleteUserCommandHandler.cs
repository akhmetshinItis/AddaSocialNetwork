using Contracts.Requests.AdminRequests.UserRequests;
using Core.Abstractions;
using Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Requests.AdminRequests.UserRequests
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
    {
        private readonly IUserServiceIdentity _userService;
        private readonly IDbContext _dbContext;

        public DeleteUserCommandHandler(IUserServiceIdentity userService, IDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }

        public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(u => u.Hobbies)
                    .ThenInclude(h => h.Photos)
                .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                return new DeleteUserResponse { Succeeded = false };
            }

            // Помечаем пользователя как удаленного
            user.IsDeleted = true;

            // Помечаем профиль как удаленный
            var profile = await _dbContext.ProfileDatas
                .FirstOrDefaultAsync(p => p.UserId == user.Id, cancellationToken);
            if (profile != null)
            {
                profile.IsDeleted = true;
            }

            // Помечаем посты как удаленные
            var posts = await _dbContext.Posts
                .Where(p => p.UserId == user.Id)
                .ToListAsync(cancellationToken);
            foreach (var post in posts)
            {
                post.IsDeleted = true;
            }

            // Помечаем комментарии как удаленные
            var comments = await _dbContext.Comments
                .Where(c => c.UserId == user.Id)
                .ToListAsync(cancellationToken);
            foreach (var comment in comments)
            {
                comment.IsDeleted = true;
            }

            // Помечаем лайки как удаленные
            var likes = await _dbContext.Likes
                .Where(l => l.UserId == user.Id)
                .ToListAsync(cancellationToken);
            foreach (var like in likes)
            {
                like.IsDeleted = true;
            }

            // Помечаем альбомы и фотографии как удаленные
            var albums = await _dbContext.Albums
                .Include(a => a.Photos)
                .Where(a => a.UserId == user.Id)
                .ToListAsync(cancellationToken);
            foreach (var album in albums)
            {
                album.IsDeleted = true;
                foreach (var photo in album.Photos)
                {
                    photo.IsDeleted = true;
                }
            }

            // Помечаем хобби и их фотографии как удаленные
            foreach (var hobby in user.Hobbies)
            {
                hobby.IsDeleted = true;
                foreach (var photo in hobby.Photos)
                {
                    photo.IsDeleted = true;
                }
            }

            // Помечаем связи друзей как удаленные
            var friendLinks = await _dbContext.Friends
                .Where(f => f.User1 == user.Id || f.User2 == user.Id)
                .ToListAsync(cancellationToken);
            foreach (var link in friendLinks)
            {
                link.IsDeleted = true;
            }

            // Помечаем категории друзей как удаленные
            var friendCategories = await _dbContext.FriendCategoryLinks
                .Where(f => f.UserId == user.Id || f.FriendId == user.Id)
                .ToListAsync(cancellationToken);
            foreach (var category in friendCategories)
            {
                category.IsDeleted = true;
            }

            // Помечаем чаты и сообщения как удаленные
            var chats = await _dbContext.Chats
                .Include(c => c.Messages)
                .Where(c => c.User1Id == user.Id || c.User2Id == user.Id)
                .ToListAsync(cancellationToken);
            foreach (var chat in chats)
            {
                chat.IsDeleted = true;
                foreach (var message in chat.Messages)
                {
                    message.IsDeleted = true;
                }
            }

            // Удаляем пользователя из Identity
            var result = await _userService.DeleteUserAsync(user.IdentityUserId);
            
            if (result.Succeeded)
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return new DeleteUserResponse
            {
                Succeeded = result.Succeeded
            };
        }
    }
}