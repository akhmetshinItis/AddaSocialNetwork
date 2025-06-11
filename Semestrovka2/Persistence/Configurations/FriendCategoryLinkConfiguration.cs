using Core.Entities;

namespace Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class FriendCategoryLinkConfiguration : IEntityTypeConfiguration<FriendCategoryLink>
    {
        public void Configure(EntityTypeBuilder<FriendCategoryLink> builder)
        {
            builder
                .HasOne(link => link.User)
                .WithMany()
                .HasForeignKey(link => link.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(link => link.Friend)
                .WithMany()
                .HasForeignKey(link => link.FriendId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(link => link.FriendCategory)
                .WithMany()
                .HasForeignKey(link => link.FriendCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}