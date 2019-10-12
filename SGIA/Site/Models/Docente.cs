using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class Docente
    {
        [Key]
        public int DocenteId { get; set; }
        public DateTime DataCadastro { get; set; }
        public int AreaAtuacaoId { get; set; }
        public int TituloId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string EmailLattes { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public double CargaHoraria { get; set; }
        public DateTime DataPosse { get; set; }
        public int TipoId { get; set; }
        public int StatusId { get; set; }
    }
}