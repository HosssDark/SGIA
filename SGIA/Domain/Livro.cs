using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Livro
    {
        [Key]
        [Display(Name = "ID")]
        public int LivroId { get; set; }

        [Display(Name = "Data de Cadastro")]
        [Required(ErrorMessage = "Obrigatório")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "Obrigatório")]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        public string Titulo { get; set; }

        [Display(Name = "Autor")]
        [Required(ErrorMessage = "Obrigatório")]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        public string Autor { get; set; }

        [Display(Name = "Editora")]
        [Required(ErrorMessage = "Obrigatório")]
        public int EditoraId { get; set; }

        [Display(Name = "Data Publicação")]
        [Required(ErrorMessage = "Obrigatório")]
        public DateTime DataPublicacao { get; set; }

        [Display(Name = "Area de Conhecimento")]
        [Required(ErrorMessage = "Obrigatório")]
        public string AreaConhecimento { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Obrigatório")]
        public int StatusId { get; set; }
    }
}