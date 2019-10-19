using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class DocenteTurmaConfig : IEntityTypeConfiguration<DocenteTurma>
    {
        public void Configure(EntityTypeBuilder<DocenteTurma> builder)
        {
            builder.HasKey(a => a.DocenteTurmaId);

            builder.ToTable("docente_turmas");
        }
    }
}