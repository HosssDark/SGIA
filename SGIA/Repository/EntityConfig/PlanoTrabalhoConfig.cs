using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class PlanoTrabalhoConfig : IEntityTypeConfiguration<PlanoTrabalho>
    {
        public void Configure(EntityTypeBuilder<PlanoTrabalho> builder)
        {
            builder.HasKey(a => a.PlanoTrabalhoId);

            builder.ToTable("planos_trabalho");
        }
    }
}