using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class TipoAcesso
    {
        [Key]
        [Display(Name = "ID")]
        public int TipoAcessoId { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}