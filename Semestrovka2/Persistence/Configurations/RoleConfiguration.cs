using Core.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
        {
            builder.HasData(
                new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = RoleConstants.User,
                    NormalizedName = RoleConstants.User.ToUpper()
                },
                new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = RoleConstants.Admin,
                    NormalizedName = RoleConstants.Admin.ToUpper()
                },
                new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = RoleConstants.Owner,
                    NormalizedName = RoleConstants.Owner.ToUpper()
                });
        }
    }
}