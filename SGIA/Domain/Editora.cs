using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Editora
    {
        [Key]
        [Display(Name = "ID")]
        public int EditoraId { get; set; }

        [Display(Name = "Data de Cadastro")]
        [Required(ErrorMessage = "Obrigatório")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Obrigatório")]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Obrigatório")]
        public int StatusId { get; set; }
    }
}