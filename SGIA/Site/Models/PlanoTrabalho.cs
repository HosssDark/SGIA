using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class PlanoTrabalho
    {
        [Key]
        public int PlanoTrabalhoId { get; set; }
        public DateTime DataCadastro { get; set; }
        public int DocenteId { get; set; }
        public string DiaSemana { get; set; }
        public string HoraInicio { get; set; }
        public string HoraEncerramento { get; set; }
        public string DescricaoAtividade { get; set; }
        public int StatusId { get; set; }
    }
}