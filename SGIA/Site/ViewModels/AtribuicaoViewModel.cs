using System;

namespace Site.ViewModels
{
    public class AtribuicaoViewModel
    {
        public int AtribuicaoId { get; set; }
        public DateTime DataCadastro { get; set; }
        public int DiciplinaId { get; set; }
        public string Diciplina { get; set; }
        public int ProjetoId { get; set; }
        public string Projeto { get; set; }
        public int DocenteId { get; set; }
        public string Docente { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
    }
}