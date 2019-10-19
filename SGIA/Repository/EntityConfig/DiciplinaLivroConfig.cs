using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class DiciplinaLivroConfig : IEntityTypeConfiguration<DiciplinaLivro>
    {
        public void Configure(EntityTypeBuilder<DiciplinaLivro> builder)
        {
            builder.HasKey(a => a.DiciplinaLivroId);

            builder.ToTable("diciplina_livros");
        }
    }
}