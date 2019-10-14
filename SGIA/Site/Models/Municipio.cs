using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class Municipio
    {
        [Key]
        [Display(Name = "ID")]
        public int MunicipioId { get; set; }

        [Display(Name = "Código")]
        public int Codigo { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }
    }
}