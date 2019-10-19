using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Diciplina
    {
        [Key]
        [Display(Name = "ID")]
        public int DiciplinaId { get; set; }

        [Display(Name = "Data Cadastro")]
        [Required]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Nome")]
        [Required]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Carga Horária")]
        [Required]
        public double CargaHoraria { get; set; }

        [Display(Name = "Quant. Horas Semanais")]
        [Required]
        public double HoraSemanal { get; set; }

        [Display(Name = "Quant. Créditos Ensino")]
        [Required]
        public int CreditoEnsino { get; set; }

        [Display(Name = "Crédito Atividade Prática")]
        [Required]
        public int CreditoAtividadePratica { get; set; }

        [Display(Name = "Crédito Atividade Campo")]
        [Required]
        public int CreditoAtividadeCampo { get; set; }

        [Display(Name = "Ementa")]
        [Required]
        public string Ementa { get; set; }

        [Required]
        [Display(Name = "Turma")]
        public int TurmaId { get; set; }

        [Required]
        [Display(Name = "Status")]
        public int StatusId { get; set; }
    }
}