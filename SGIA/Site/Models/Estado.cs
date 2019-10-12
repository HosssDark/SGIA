using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class Estado
    {
        [Key]
        public int EstadoId { get; set; }
        public string UF { get; set; }
        public string Nome { get; set; }
    }
}