using Domain;

namespace Site
{
    public class UserViewModel
    {
        public User User { get; set; }
        public UserPassword Password { get; set; }
        public Endereco Endereco { get; set; }
        public string PasswordConfirm { get; set; }
        public string AreaAtuacao { get; set; }
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public string TipoAcesso { get; set; }
        public byte[] Image { get; set; }
        public string Status { get; set; }
        public string Classe { get; set; }
        public string Cor { get; set; }
    }
}