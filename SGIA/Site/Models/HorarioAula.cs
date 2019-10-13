using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class HorarioAula
    {
        [Key]
        [Display(Name = "ID")]
        public int HorarioAulaId { get; set; }

        [Display(Name = "Data de Cadastro")]
        [Required]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Dia da Semana")]
        [Required]
        public string DiaSemana { get; set; }

        [Display(Name = "Diciplina Primeiro Horário")]
        [Required]
        public int DiciplinaPrimeiroId { get; set; }

        [Display(Name = "Diciplina Segundo Horário")]
        [Required]
        public int DiciplinaSegundoId { get; set; }

        [Display(Name = "Diciplina Terceiro Horário")]
        [Required]
        public int DiciplinaTerceiroId { get; set; }

        [Display(Name = "Diciplina Quarto Horário")]
        [Required]
        public int DiciplinaQuartoId { get; set; }

        [Display(Name = "Turma")]
        [Required]
        public int TurmaId { get; set; }

        [Display(Name = "Periodo")]
        [Required]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        public string Periodo { get; set; }

        [Display(Name = "Status")]
        [Required]
        public int StatusId { get; set; }
    }
}