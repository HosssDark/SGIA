using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class AreaAtuacao
    {
        [Key]
        public int AreaAtuacaoId { get; set; }
        public string Descricao { get; set; }
    }
}