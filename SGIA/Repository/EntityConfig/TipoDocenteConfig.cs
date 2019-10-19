using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class TipoDocenteConfig : IEntityTypeConfiguration<TipoDocente>
    {
        public void Configure(EntityTypeBuilder<TipoDocente> builder)
        {
            builder.HasKey(a => a.TipoDocenteId);

            builder.ToTable("tipos_docente");
        }
    }
}