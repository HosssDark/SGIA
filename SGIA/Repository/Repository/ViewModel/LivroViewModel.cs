using System;

namespace Repository.Repository.ViewModel
{
    public class LivroViewModel
    {
        public int LivroId { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int EditoraId { get; set; }
        public string Editora { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string AreaConhecimento { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public string StatusIcon { get; set; }
        public string Image { get; set; }
    }
}