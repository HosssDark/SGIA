using System.ComponentModel.DataAnnotations;

namespace Domain
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