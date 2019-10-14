using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class Estado
    {
        [Key]
        [Display(Name = "ID")]
        public int EstadoId { get; set; }

        [Display(Name = "UF")]
        public string UF { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }
    }
}