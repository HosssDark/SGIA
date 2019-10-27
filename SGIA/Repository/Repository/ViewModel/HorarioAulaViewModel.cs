using System;

namespace Repository.Repository.ViewModel
{
    public class HorarioAulaViewModel
    {
        public int HorarioAulaId { get; set; }
        public DateTime DataCadastro { get; set; }
        public string DiaSemana { get; set; }
        public int DiciplinaPrimeiroId { get; set; }
        public string DiciplinaPrimeiro { get; set; }
        public int DiciplinaSegundoId { get; set; }
        public string DiciplinaSegundo { get; set; }
        public int DiciplinaTerceiroId { get; set; }
        public string DiciplinaTerceiro { get; set; }
        public int DiciplinaQuartoId { get; set; }
        public string DiciplinaQuarto { get; set; }
        public int TurmaId { get; set; }
        public string Turma { get; set; }
        public string Periodo { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
    }
}