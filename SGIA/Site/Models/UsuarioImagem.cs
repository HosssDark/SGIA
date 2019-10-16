using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class UsuarioImagem
    {
        [Key]
        public int ImagemId { get; set; }
        public int UsuarioId { get; set; }
        public string Path { get; set; }
        public string PathVirtual { get; set; }
    }
}