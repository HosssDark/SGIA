using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class TurmaConfig : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.HasKey(a => a.TurmaId);

            builder.ToTable("turmas");
        }
    }
}