using System.ComponentModel.DataAnnotations;

namespace Domain
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