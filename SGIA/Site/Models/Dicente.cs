using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class Dicente
    {
        [Key]
        [Display(Name = "ID")]
        public int DicenteId { get; set; }

        [Display(Name = "Data de Cadastro")]
        [Required]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Matricula")]
        public int Matricula { get; set; }

        [Display(Name = "Nome")]
        [Required]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Telefone")]
        [MaxLength(15, ErrorMessage = "Máximo de 15 caracteres")]
        public string Telefone { get; set; }

        [Display(Name = "Celular")]
        [MaxLength(15, ErrorMessage = "Máximo de 15 caracteres")]
        public string Celular { get; set; }

        [Display(Name = "Email")]
        [MaxLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        public string Email { get; set; }

        public int StatusId { get; set; }
    }
}