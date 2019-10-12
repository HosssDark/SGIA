using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class DiciplinaLivro
    {
        [Key]
        public int DiciplinaLivroId { get; set; }
        public DateTime DataCadastro { get; set; }
        public int DiciplinaId { get; set; }
        public int LivroId { get; set; }
        public int StatusId { get; set; }
    }
}