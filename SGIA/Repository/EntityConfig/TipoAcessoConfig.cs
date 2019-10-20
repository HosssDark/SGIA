using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class TipoAcessoConfig : IEntityTypeConfiguration<TipoAcesso>
    {
        public void Configure(EntityTypeBuilder<TipoAcesso> builder)
        {
            builder.HasKey(a => a.TipoAcessoId);

            builder.ToTable("tipos_acesso");
        }
    }
}