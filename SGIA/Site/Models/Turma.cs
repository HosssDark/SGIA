using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class Turma
    {
        [Key]
        [Display(Name = "ID")]
        public int TurmaId { get; set; }

        [Display(Name = "Data de Cadastro")]
        [Required]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Nome")]
        [Required]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        public string Nome { get; set; }
    }
}