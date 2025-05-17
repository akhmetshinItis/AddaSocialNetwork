using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Abstractions ;

    public interface IDbContext
    {
        DbSet<User> Users { get; set; }
    }