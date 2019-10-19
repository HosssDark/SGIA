using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class DiciplinaConfig : IEntityTypeConfiguration<Diciplina>
    {
        public void Configure(EntityTypeBuilder<Diciplina> builder)
        {
            builder.HasKey(a => a.DiciplinaId);

            builder.ToTable("diciplinas");
        }
    }
}