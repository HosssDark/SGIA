using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Docente
    {
        [Key]
        [Display(Name = "ID")]
        public int DocenteId { get; set; }

        [Display(Name = "Data de Cadastro")]
        [Required]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Area de Atuação")]
        [Required]
        public int AreaAtuacaoId { get; set; }

        [Display(Name = "Titulo")]
        [Required]
        public int TituloId { get; set; }

        [Display(Name = "Nome")]
        [Required]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Email")]
        [Required]
        [MaxLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        public string Email { get; set; }

        [Display(Name = "Email Lattes")]
        [Required]
        [MaxLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        public string EmailLattes { get; set; }

        [Display(Name = "Data Nascimento")]
        [Required]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Telefone")]
        [Required]
        [MaxLength(15, ErrorMessage = "Máximo de 15 caracteres")]
        public string Telefone { get; set; }

        [Display(Name = "Celular")]
        [Required]
        [MaxLength(15, ErrorMessage = "Máximo de 15 caracteres")]
        public string Celular { get; set; }

        [Display(Name = "Carga Horária")]
        [Required]
        public double CargaHoraria { get; set; }

        [Display(Name = "Data Posse")]
        [Required]
        public DateTime DataPosse { get; set; }

        [Display(Name = "Tipo")]
        [Required]
        public int TipoId { get; set; }

        [Display(Name = "Status")]
        [Required]
        public int StatusId { get; set; }
    }
}