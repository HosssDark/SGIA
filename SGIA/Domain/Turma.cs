using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Turma
    {
        [Key]
        [Display(Name = "ID")]
        public int TurmaId { get; set; }

        [Display(Name = "Data de Cadastro")]
        [Required]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Nome")]
        [Required]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Semestres")]
        [Required]
        public int QtdeSemestres { get; set; }

        [Display(Name = "Duração")]
        [Required]
        public int Duracao { get; set; }

        [Display(Name = "Coordenador")]
        [Required]
        public int CoordenadorId { get; set; }

        [Display(Name = "Status")]
        [Required]
        public int StatusId { get; set; }
    }
}