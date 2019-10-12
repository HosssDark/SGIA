using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class Municipio
    {
        [Key]
        public int MunicipioId { get; set; }
        public int Codigo { get; set; }
        public string Nome { get; set; }
    }
}