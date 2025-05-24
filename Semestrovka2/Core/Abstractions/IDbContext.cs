using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Abstractions ;

    public interface IDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Friend> Friends { get; }
        
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }