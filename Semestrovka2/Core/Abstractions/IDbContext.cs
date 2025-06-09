using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Core.Abstractions ;

    public interface IDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Friend> Friends { get; }
        DbSet<Message> Messages { get; }
        DbSet<Chat> Chats { get; }
        DbSet<FriendCategory> FriendCategories { get; }
        DbSet<FriendCategoryLink> FriendCategoryLinks { get; }
        DbSet<ProfileData> ProfileDatas { get; }
        DbSet<Post> Posts { get; }
        DbSet<Comment> Comments { get; }
        DbSet<Like> Likes { get; }
        DbSet<Photo> Photos { get; }
        DbSet<Album> Albums { get; }
        
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        
        public DatabaseFacade Database { get; }
    }