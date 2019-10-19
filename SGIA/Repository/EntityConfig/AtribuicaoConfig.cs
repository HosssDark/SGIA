using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class AtribuicaoConfig : IEntityTypeConfiguration<Atribuicao>
    {
        public void Configure(EntityTypeBuilder<Atribuicao> builder)
        {
            builder.HasKey(a => a.AtribuicaoId);

            builder.ToTable("atribuicoes");
        }
    }
}