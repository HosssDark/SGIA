using System;

namespace Repository.Repository.ViewModel
{
    public class DicenteViewModel
    {
        public int DicenteId { get; set; }
        public DateTime DataCadastro { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public string Image { get; set; }
    }
}