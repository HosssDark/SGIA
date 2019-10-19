using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class TituloConfig : IEntityTypeConfiguration<Titulo>
    {
        public void Configure(EntityTypeBuilder<Titulo> builder)
        {
            builder.HasKey(a => a.TituloId);

            builder.ToTable("titulos");
        }
    }
}