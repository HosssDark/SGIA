using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class Turma
    {
        [Key]
        public int TurmaId { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Nome { get; set; }
    }
}