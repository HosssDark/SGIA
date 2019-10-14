using System.ComponentModel.DataAnnotations;

namespace Site.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Lembre de Mim")]
        public bool LembreMim { get; set; }
    }
}