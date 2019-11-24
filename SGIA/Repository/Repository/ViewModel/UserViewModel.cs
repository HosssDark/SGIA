using System;

namespace Repository.Repository.ViewModel
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public DateTime DataCadastro { get; set; }
        public int? AreaAtuacaoId { get; set; }
        public string AreaAtuacao { get; set; }
        public int? TituloId { get; set; }
        public string Titulo { get; set; }
        public int? CargoId { get; set; }
        public string Cargo { get; set; }
        public int? TipoId { get; set; }
        public string Tipo { get; set; }
        public int TipoAcessoId { get; set; }
        public string TipoAcesso { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string EmailLattes { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public double CargaHoraria { get; set; }
        public DateTime? DataPosse { get; set; }
        public bool LembreMim { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public string Image { get; set; }
    }
}