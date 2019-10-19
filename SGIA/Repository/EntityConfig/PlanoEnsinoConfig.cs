using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class PlanoEnsinoConfig : IEntityTypeConfiguration<PlanoEnsino>
    {
        public void Configure(EntityTypeBuilder<PlanoEnsino> builder)
        {
            builder.HasKey(a => a.PlanoEnsinoId);

            builder.ToTable("planos_ensino");
        }
    }
}