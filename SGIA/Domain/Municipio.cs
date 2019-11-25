using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Municipio
    {
        [Key]
        [Display(Name = "ID")]
        public int MunicipioId { get; set; }

        [Display(Name = "UF")]
        public string Uf { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }
    }
}