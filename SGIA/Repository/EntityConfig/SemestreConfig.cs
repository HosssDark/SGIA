using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class SemestreConfig : IEntityTypeConfiguration<Semestre>
    {
        public void Configure(EntityTypeBuilder<Semestre> builder)
        {
            builder.HasKey(a => a.SemestreId);

            builder.ToTable("semestres");
        }
    }
}