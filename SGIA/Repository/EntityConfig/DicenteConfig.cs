using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class DicenteConfig : IEntityTypeConfiguration<Dicente>
    {
        public void Configure(EntityTypeBuilder<Dicente> builder)
        {
            builder.HasKey(a => a.DicenteId);

            builder.ToTable("dicentes");
        }
    }
}