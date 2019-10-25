using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Atribuicao
    {
        [Key]
        [Display(Name = "ID")]
        public int AtribuicaoId { get; set; }

        [Display(Name = "Data de Cadastro")]
        [Required(ErrorMessage = "Obrigatório")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Diciplina")]
        [Required(ErrorMessage = "Obrigatório")]
        public int DiciplinaId { get; set; }

        [Display(Name = "Projeto")]
        [Required(ErrorMessage = "Obrigatório")]
        public int ProjetoId { get; set; }

        [Display(Name = "Docente")]
        [Required(ErrorMessage = "Obrigatório")]
        public int UserId { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Obrigatório")]
        public int StatusId { get; set; }
    }
}