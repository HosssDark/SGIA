using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class UserImageConfig : IEntityTypeConfiguration<UserImage>
    {
        public void Configure(EntityTypeBuilder<UserImage> builder)
        {
            builder.HasKey(a => a.ImagemId);

            builder.ToTable("user_image");
        }
    }
}