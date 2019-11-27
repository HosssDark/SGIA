using Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Site
{
    public class UserViewModel
    {
        public User User { get; set; }
        public UserPassword Password { get; set; }
        public Address Address { get; set; } = new Address();
        public ChangePasswordViewModel ChangePassword { get; set; } = new ChangePasswordViewModel();
        public string AreaAtuacao { get; set; }
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public string TipoAcesso { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public string Classe { get; set; }
        public string Cor { get; set; }

        [Display(Name = "Imagem (700x500)")]
        public IFormFile File { get; set; }
    }

    public class ChangePasswordViewModel
    {
        public int UserId { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        public string ConfirmPassword { get; set; }
    }

    public class UserReportViewModel
    {
        public int UserId { get; set; }
        public int StatusId { get; set; }
        public string Formato { get; set; } = "pdf";
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
    }
}