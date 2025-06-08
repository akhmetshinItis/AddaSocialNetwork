using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ProfileDataConfiguration : IEntityTypeConfiguration<ProfileData>
    {
        public void Configure(EntityTypeBuilder<ProfileData> builder)
        {
            builder.Property(u => u.BackgroundImage)
                .HasDefaultValue("assets/images/banner/profile-banner.jpg");
        }
    }
}