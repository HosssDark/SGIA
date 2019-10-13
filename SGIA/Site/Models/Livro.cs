using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class Livro
    {
        [Key]
        [Display(Name = "ID")]
        public int LivroId { get; set; }

        [Display(Name = "Data de Cadastro")]
        [Required]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Título")]
        [Required]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        public string Titulo { get; set; }

        [Display(Name = "Autor")]
        [Required]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        public string Autor { get; set; }

        [Display(Name = "Editora")]
        [Required]
        public int EditoraId { get; set; }

        [Display(Name = "Data Publicação")]
        [Required]
        public DateTime DataPublicacao { get; set; }

        [Display(Name = "Area de Conhecimento")]
        [Required]
        public string AreaConhecimento { get; set; }

        [Display(Name = "Status")]
        [Required]
        public int StatusId { get; set; }
    }
}