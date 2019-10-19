using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class User
    {
        [Key]
        [Display(Name = "ID")]
        public int UserId { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Tipo de Acesso")]
        public int TipoAcesso { get; set; }

        [Display(Name = "Lembre de Mim")]
        public bool LembreMim { get; set; }

        [Display(Name = "Data Cadastro")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }
    }
}