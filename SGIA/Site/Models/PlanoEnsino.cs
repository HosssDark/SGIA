using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class PlanoEnsino
    {
        [Key]
        public int PlanoEnsinoId { get; set; }
        public DateTime DataCadastro { get; set; }
        public int DiciplinaId { get; set; }
        public int DocenteId { get; set; }
        public int TurmaId { get; set; }
        public string ObjetivoArea { get; set; }
        public string ObjetivoGeral { get; set; }
        public int EmentaId { get; set; }
        public string EspecificacaoConteudo { get; set; }
        public string MetodologiaAvaliacao { get; set; }
        public string TecnicaPedagogica { get; set; }
        public string RecursoUtilizado { get; set; }
        public string AtividadeTrabalhada { get; set; }
        public DateTime DataEmissao { get; set; }
        public int StatusId { get; set; }
    }
}