using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Projeto
    {
        [Key]
        [Display(Name = "ID")]
        public int ProjetoId { get; set; }

        [Display(Name = "Data de Cadastro")]
        [Required]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Docente")]
        [Required]
        public int UserId { get; set; }

        [Display(Name = "Nome")]
        [Required]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Carga Horária")]
        [Required]
        public double CargaHoraria { get; set; }

        [Display(Name = "Data de Inicio")]
        [Required]
        public DateTime DataInicio { get; set; }

        [Display(Name = "Data de Término")]
        [Required]
        public DateTime DataTermino { get; set; }

        [Display(Name = "Status")]
        [Required]
        public int StatusId { get; set; }
    }
}