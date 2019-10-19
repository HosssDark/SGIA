using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class DocenteConfig : IEntityTypeConfiguration<Docente>
    {
        public void Configure(EntityTypeBuilder<Docente> builder)
        {
            builder.HasKey(a => a.DocenteId);

            builder.ToTable("docentes");
        }
    }
}