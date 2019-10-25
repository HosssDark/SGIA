using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class AreaAtuacaoConfig : IEntityTypeConfiguration<AreaAtuacao>
    {
        public void Configure(EntityTypeBuilder<AreaAtuacao> builder)
        {
            builder.HasKey(a => a.AreaAtuacaoId);

            builder.ToTable("areas_atuacao");
        }
    }
}