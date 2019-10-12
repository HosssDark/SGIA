using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class HorarioAula
    {
        [Key]
        public int HorarioAulaId { get; set; }
        public DateTime DataCadastro { get; set; }
        public string DiaSemana { get; set; }
        public int DiciplinaPrimeiroId { get; set; }
        public int DiciplinaSegundoId { get; set; }
        public int DiciplinaTerceiroId { get; set; }
        public int DiciplinaQuartoId { get; set; }
        public int TurmaId { get; set; }
        public string Periodo { get; set; }
        public int StatusId { get; set; }
    }
}