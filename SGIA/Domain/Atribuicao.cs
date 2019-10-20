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
        [Required]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Diciplina")]
        [Required]
        public int DiciplinaId { get; set; }

        [Display(Name = "Projeto")]
        [Required]
        public int ProjetoId { get; set; }

        [Display(Name = "Docente")]
        [Required]
        public int UserId { get; set; }

        [Display(Name = "Status")]
        [Required]
        public int StatusId { get; set; }
    }
}