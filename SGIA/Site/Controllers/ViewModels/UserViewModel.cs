using System;

namespace Site
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Nome { get; set; }
        public string Profissao { get; set; }
        public string Email { get; set; }
        public int TipoAcessoId { get; set; }
        public string TipoAcesso { get; set; }
        public string Educacao { get; set; }
        public string Habilidades { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
    }
}