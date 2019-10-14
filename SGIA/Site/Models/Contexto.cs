using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Site.Models
{
    public class Contexto : DbContext
    {
        public DbSet<AreaAtuacao> AreasAtuacao { get; set; }
        public DbSet<Atribuicao> Atribuicoes { get; set; }
        public DbSet<Dicente> Dicentes { get; set; }
        public DbSet<Diciplina> Diciplinas { get; set; }
        public DbSet<DiciplinaLivro> DiciplinaLivros { get; set; }
        public DbSet<Docente> Docentes { get; set; }
        public DbSet<DocenteTurma> DocenteTurmas { get; set; }
        public DbSet<Editora> Editoras { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<HorarioAula> HorarioAulas { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<PlanoEnsino> PlanosEnsino { get; set; }
        public DbSet<PlanoTrabalho> PlanosTrabalho { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<TipoDocente> TiposDocente { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Titulo> Titulos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

                var Configuration = builder.Build();

                optionsBuilder.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
            }
        }
    }
}