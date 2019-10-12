using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class Livro
    {
        [Key]
        public int LivroId { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int EditoraId { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string AreaConhecimento { get; set; }
        public int StatusId { get; set; }
    }
}