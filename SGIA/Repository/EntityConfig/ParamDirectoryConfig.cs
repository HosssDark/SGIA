using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class ParamDirectoryConfig : IEntityTypeConfiguration<ParamDirectory>
    {
        public void Configure(EntityTypeBuilder<ParamDirectory> builder)
        {
            builder.HasKey(a => a.ParamId);

            builder.ToTable("param_directory");
        }
    }
}