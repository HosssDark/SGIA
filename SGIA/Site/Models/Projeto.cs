using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class Projeto
    {
        [Key]
        public int ProjetoId { get; set; }
        public DateTime DataCadastro { get; set; }
        public int DocenteId { get; set; }
        public string Nome { get; set; }
        public double CargaHoraria { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public int StatusId { get; set; }
    }
}