using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class Diciplina
    {
        [Key]
        public int DicipilinaId { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Nome { get; set; }
        public double CargaHoraria { get; set; }
        public double HoraSemanal { get; set; }
        public int CreditoEnsino { get; set; }
        public int CreditoAtividadePratica { get; set; }
        public int CreditoAtividadeCampo { get; set; }
        public string Ementa { get; set; }
        public int TurmaId { get; set; }
        public int StatusId { get; set; }
    }
}