using System.ComponentModel.DataAnnotations;

namespace Site
{
    public class ToRecoverViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}