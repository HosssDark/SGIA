﻿using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.EntityConfig;
using System.IO;

namespace Repository
{
    public class Context : DbContext
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
        public DbSet<User> Usuarios { get; set; }
        public DbSet<UserImage> UsuarioImagens { get; set; }
        public DbSet<UserPassword> UsersPassword { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

                var Configuration = builder.Build();

                optionsBuilder.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<AreaAtuacao>(new AreaAtuacaoConfig());
            modelBuilder.ApplyConfiguration<Atribuicao>(new AtribuicaoConfig());
            modelBuilder.ApplyConfiguration<Dicente>(new DicenteConfig());
            modelBuilder.ApplyConfiguration<Diciplina>(new DiciplinaConfig());
            modelBuilder.ApplyConfiguration<DiciplinaLivro>(new DiciplinaLivroConfig());
            modelBuilder.ApplyConfiguration<Docente>(new DocenteConfig());
            modelBuilder.ApplyConfiguration<DocenteTurma>(new DocenteTurmaConfig());
            modelBuilder.ApplyConfiguration<Editora>(new EditoraConfig());
            modelBuilder.ApplyConfiguration<Endereco>(new EnderecoConfig());
            modelBuilder.ApplyConfiguration<HorarioAula>(new HorarioAulaConfig());
            modelBuilder.ApplyConfiguration<Livro>(new LivroConfig());
            modelBuilder.ApplyConfiguration<PlanoEnsino>(new PlanoEnsinoConfig());
            modelBuilder.ApplyConfiguration<PlanoTrabalho>(new PlanoTrabalhoConfig());
            modelBuilder.ApplyConfiguration<Projeto>(new ProjetoConfig());
            modelBuilder.ApplyConfiguration<TipoDocente>(new TipoDocenteConfig());
            modelBuilder.ApplyConfiguration<Turma>(new TurmaConfig());
            modelBuilder.ApplyConfiguration<Menu>(new MenuConfig());
            modelBuilder.ApplyConfiguration<Status>(new StatusConfig());
            modelBuilder.ApplyConfiguration<Titulo>(new TituloConfig());
            modelBuilder.ApplyConfiguration<User>(new UserConfig());
            modelBuilder.ApplyConfiguration<UserImage>(new UserImageConfig());
            modelBuilder.ApplyConfiguration<UserPassword>(new UserPasswordConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}