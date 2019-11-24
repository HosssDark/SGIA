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
        [Required(ErrorMessage = "Obrigatório")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Obrigatório")]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Carga Horária")]
        [Required(ErrorMessage = "Obrigatório")]
        public double CargaHoraria { get; set; }

        [Display(Name = "Quant. Horas Semanais")]
        [Required(ErrorMessage = "Obrigatório")]
        public double HoraSemanal { get; set; }

        [Display(Name = "Quant. Créditos Ensino")]
        [Required(ErrorMessage = "Obrigatório")]
        public int CreditoEnsino { get; set; }

        [Display(Name = "Crédito Atividade Prática")]
        [Required(ErrorMessage = "Obrigatório")]
        public int CreditoAtividadePratica { get; set; }

        [Display(Name = "Crédito Atividade Campo")]
        [Required(ErrorMessage = "Obrigatório")]
        public int CreditoAtividadeCampo { get; set; }

        [Display(Name = "Ementa")]
        [Required(ErrorMessage = "Obrigatório")]
        public string Ementa { get; set; }

        [Display(Name = "Turma")]
        [Required(ErrorMessage = "Obrigatório")]
        public int TurmaId { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        [Display(Name = "Status")]
        public int StatusId { get; set; }
    }
}