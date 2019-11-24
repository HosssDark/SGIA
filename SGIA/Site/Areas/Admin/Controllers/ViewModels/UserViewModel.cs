using Domain;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Site
{
    public class UserViewModel
    {
        public User User { get; set; }
        public UserPassword Password { get; set; }
        public Address Address { get; set; }
        public ChangePasswordViewModel ChangePassword { get; set; }
        public string AreaAtuacao { get; set; }
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public string TipoAcesso { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public string Classe { get; set; }
        public string Cor { get; set; }
        public IFormFile File { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        public string ConfirmPassword { get; set; }
    }
}