using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class Editora
    {
        [Key]
        [Display(Name = "ID")]
        public int EditoraId { get; set; }

        [Display(Name = "Data de Cadastro")]
        [Required]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Nome")]
        [Required]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Status")]
        [Required]
        public int StatusId { get; set; }
    }
}