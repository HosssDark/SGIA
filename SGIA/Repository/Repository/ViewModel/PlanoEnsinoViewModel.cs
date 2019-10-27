using System;

namespace Repository.Repository.ViewModel
{
    public class PlanoEnsinoViewModel
    {
        public int PlanoEnsinoId { get; set; }
        public DateTime DataCadastro { get; set; }
        public int DiciplinaId { get; set; }
        public string Diciplina { get; set; }
        public int UserId { get; set; }
        public string Docente { get; set; }
        public int TurmaId { get; set; }
        public string Turma { get; set; }
        public string ObjetivoArea { get; set; }
        public string ObjetivoGeral { get; set; }
        public int EmentaId { get; set; }
        public string Ementa { get; set; }
        public string EspecificacaoConteudo { get; set; }
        public string MetodologiaAvaliacao { get; set; }
        public string TecnicaPedagogica { get; set; }
        public string RecursoUtilizado { get; set; }
        public string AtividadeTrabalhada { get; set; }
        public int BiografiaBasicaId { get; set; }
        public string BiografiaBasica { get; set; }
        public int BiografiaComplementarId { get; set; }
        public string BiografiaComplementar { get; set; }
        public DateTime DataEmissao { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
    }
}