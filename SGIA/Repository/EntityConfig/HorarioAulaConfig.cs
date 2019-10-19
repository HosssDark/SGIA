using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class HorarioAulaConfig : IEntityTypeConfiguration<HorarioAula>
    {
        public void Configure(EntityTypeBuilder<HorarioAula> builder)
        {
            builder.HasKey(a => a.HorarioAulaId);

            builder.ToTable("horario_aulas");
        }
    }
}