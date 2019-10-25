using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class HorarioAula
    {
        [Key]
        [Display(Name = "ID")]
        public int HorarioAulaId { get; set; }

        [Display(Name = "Data de Cadastro")]
        [Required(ErrorMessage = "Obrigatório")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Dia da Semana")]
        [Required(ErrorMessage = "Obrigatório")]
        public string DiaSemana { get; set; }

        [Display(Name = "Diciplina Primeiro Horário")]
        [Required(ErrorMessage = "Obrigatório")]
        public int DiciplinaPrimeiroId { get; set; }

        [Display(Name = "Diciplina Segundo Horário")]
        [Required(ErrorMessage = "Obrigatório")]
        public int DiciplinaSegundoId { get; set; }

        [Display(Name = "Diciplina Terceiro Horário")]
        [Required(ErrorMessage = "Obrigatório")]
        public int DiciplinaTerceiroId { get; set; }

        [Display(Name = "Diciplina Quarto Horário")]
        [Required(ErrorMessage = "Obrigatório")]
        public int DiciplinaQuartoId { get; set; }

        [Display(Name = "Turma")]
        [Required(ErrorMessage = "Obrigatório")]
        public int TurmaId { get; set; }

        [Display(Name = "Periodo")]
        [Required(ErrorMessage = "Obrigatório")]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        public string Periodo { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Obrigatório")]
        public int StatusId { get; set; }
    }
}