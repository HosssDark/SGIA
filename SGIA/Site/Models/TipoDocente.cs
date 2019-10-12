using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class TipoDocente
    {
        [Key]
        public int TipoDocenteId { get; set; }
        public string Descricao { get; set; }
    }
}