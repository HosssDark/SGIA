using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class PlanoTrabalho
    {
        [Key]
        [Display(Name = "ID")]
        public int PlanoTrabalhoId { get; set; }

        [Display(Name = "Data de Cadastro")]
        [Required]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Docente")]
        [Required]
        public int DocenteId { get; set; }

        [Display(Name = "Dia da Semana")]
        [Required]
        public string DiaSemana { get; set; }

        [Display(Name = "Horário de Inicio")]
        [Required]
        [MaxLength(5, ErrorMessage = "Máximo de 5 caracteres")]
        public string HoraInicio { get; set; }

        [Display(Name = "Horário de Encerramento")]
        [Required]
        [MaxLength(5, ErrorMessage = "Máximo de 5 caracteres")]
        public string HoraEncerramento { get; set; }

        [Display(Name = "Descrição das Atividades")]
        [Required]
        public string DescricaoAtividade { get; set; }

        [Display(Name = "Status")]
        [Required]
        public int StatusId { get; set; }
    }
}