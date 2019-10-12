using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class Editora
    {
        [Key]
        public int EditoraId { get; set; }
        public string Nome { get; set; }
        public DateTime DataCadastro { get; set; }
        public int StatusId { get; set; }
    }
}