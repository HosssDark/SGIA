using System;

namespace Site
{
    public class PlanoTrabalhoViewModel
    {
        public int PlanoTrabalhoId { get; set; }
        public DateTime DataCadastro { get; set; }
        public int DocenteId { get; set; }
        public string Docente { get; set; }
        public string DiaSemana { get; set; }
        public string HoraInicio { get; set; }
        public string HoraEncerramento { get; set; }
        public string DescricaoAtividade { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
    }
}