using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class DiciplinaLivro
    {
        [Key]
        [Display(Name = "ID")]
        public int DiciplinaLivroId { get; set; }

        [Display(Name = "Data de Cadastro")]
        [Required]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Diciplina")]
        [Required]
        public int DiciplinaId { get; set; }

        [Display(Name = "Livro")]
        [Required]
        public int LivroId { get; set; }

        [Display(Name = "Status")]
        [Required]
        public int StatusId { get; set; }
    }
}