using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class EditoraConfig : IEntityTypeConfiguration<Editora>
    {
        public void Configure(EntityTypeBuilder<Editora> builder)
        {
            builder.HasKey(a => a.EditoraId);

            builder.ToTable("editoras");
        }
    }
}