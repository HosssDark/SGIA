using System;

namespace Repository.Repository.ViewModel
{
    public class DiciplinaViewModel
    {
        public int DiciplinaId { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Nome { get; set; }
        public double CargaHoraria { get; set; }
        public double HoraSemanal { get; set; }
        public int CreditoEnsino { get; set; }
        public int CreditoAtividadePratica { get; set; }
        public int CreditoAtividadeCampo { get; set; }
        public string Ementa { get; set; }
        public int TurmaId { get; set; }
        public string Turma { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public string Image { get; set; }
    }
}