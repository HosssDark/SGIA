using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class User
    {
        [Key]
        [Display(Name = "ID")]
        public int UserId { get; set; }

        [Display(Name = "Data de Cadastro")]
        [Required]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Area de Atuação")]
        public int? AreaAtuacaoId { get; set; }

        [Display(Name = "Titulo")]
        public int? TituloId { get; set; }

        [Display(Name = "Nome")]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Email")]
        [Required]
        [MaxLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        public string Email { get; set; }

        [Display(Name = "Email Lattes")]
        [MaxLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        public string EmailLattes { get; set; }

        [Display(Name = "Data Nascimento")]
        public DateTime? DataNascimento { get; set; }

        [Display(Name = "Telefone")]
        [MaxLength(15, ErrorMessage = "Máximo de 15 caracteres")]
        public string Telefone { get; set; }

        [Display(Name = "Celular")]
        [MaxLength(15, ErrorMessage = "Máximo de 15 caracteres")]
        public string Celular { get; set; }

        [Display(Name = "Carga Horária")]
        public double CargaHoraria { get; set; }

        [Display(Name = "Data Posse")]
        public DateTime? DataPosse { get; set; }

        [Display(Name = "Tipo")]
        public int? TipoId { get; set; }

        [Display(Name = "Status")]
        [Required]
        public int StatusId { get; set; }

        [Display(Name = "Tipo de Acesso")]
        [Required]
        public int TipoAcessoId { get; set; }

        [Display(Name = "Lembre de Mim")]
        public bool LembreMim { get; set; }
    }
}