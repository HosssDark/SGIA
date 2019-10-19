using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class UserPasswordConfig : IEntityTypeConfiguration<UserPassword>
    {
        public void Configure(EntityTypeBuilder<UserPassword> builder)
        {
            builder.HasKey(a => a.UserId);

            builder.ToTable("user_password");
        }
    }
}