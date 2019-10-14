using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class TipoDocente
    {
        [Key]
        [Display(Name = "ID")]
        public int TipoDocenteId { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}