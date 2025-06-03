using Core.Constants;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class FriendCategoryConfiguration : IEntityTypeConfiguration<FriendCategory>
    {
        public void Configure(EntityTypeBuilder<FriendCategory> builder)
        {
            builder.HasData(
                new FriendCategory
                {
                    Id = Guid.Parse("64ac57a0-1fcc-4e20-9ee4-2035bc6d787c"),
                    CategoryName = FriendCategoryConstants.DefaultCategory
                },
                new FriendCategory
                {
                    Id = Guid.Parse("eca5a630-fb54-4c1c-9b88-c4979ffa04c7"),
                    CategoryName = FriendCategoryConstants.Closes
                },
                new FriendCategory
                {
                    Id = Guid.Parse("5b357d96-741f-4957-bd3a-fb70adf0a5fa"),
                    CategoryName = FriendCategoryConstants.Relatives
                },
                new FriendCategory
                {
                    Id = Guid.Parse("b625b3a1-589e-4cff-916f-4a4dde52809c"),
                    CategoryName = FriendCategoryConstants.Schools
                });
        }
    }
}