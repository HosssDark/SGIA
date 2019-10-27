using System;

namespace Repository.Repository.ViewModel
{
    public class EditoraViewModel
    {
        public int EditoraId { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Nome { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
    }
}