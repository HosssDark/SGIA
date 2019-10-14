using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class Titulo
    {
        [Key]
        [Display(Name = "ID")]
        public int TituloId { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}