using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class UserImage
    {
        [Key]
        public int ImagemId { get; set; }
        public int UsuarioId { get; set; }
        public string Path { get; set; }
        public string PathVirtual { get; set; }
    }
}