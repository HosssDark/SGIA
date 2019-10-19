using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class ProjetoConfig : IEntityTypeConfiguration<Projeto>
    {
        public void Configure(EntityTypeBuilder<Projeto> builder)
        {
            builder.HasKey(a => a.ProjetoId);

            builder.ToTable("projetos");
        }
    }
}