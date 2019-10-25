using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class PlanoEnsino
    {
        [Key]
        [Display(Name = "ID")]
        public int PlanoEnsinoId { get; set; }

        [Display(Name = "Data de Cadastro")]
        [Required(ErrorMessage = "Obrigatório")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Diciplina")]
        [Required(ErrorMessage = "Obrigatório")]
        public int DiciplinaId { get; set; }

        [Display(Name = "Docente")]
        [Required(ErrorMessage = "Obrigatório")]
        public int UserId { get; set; }

        [Display(Name = "Turma")]
        [Required(ErrorMessage = "Obrigatório")]
        public int TurmaId { get; set; }

        [Display(Name = "Objetivo da Área")]
        [Required(ErrorMessage = "Obrigatório")]
        public string ObjetivoArea { get; set; }

        [Display(Name = "Objetivo Geral")]
        [Required(ErrorMessage = "Obrigatório")]
        public string ObjetivoGeral { get; set; }

        [Display(Name = "Ementa")]
        [Required(ErrorMessage = "Obrigatório")]
        public int EmentaId { get; set; }

        [Display(Name = "Especificação do Conteúdo Programático")]
        [Required(ErrorMessage = "Obrigatório")]
        public string EspecificacaoConteudo { get; set; }

        [Display(Name = "Metodologia para Avaliação")]
        [Required(ErrorMessage = "Obrigatório")]
        public string MetodologiaAvaliacao { get; set; }

        [Display(Name = "Técnica Pedagógica")]
        [Required(ErrorMessage = "Obrigatório")]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        public string TecnicaPedagogica { get; set; }

        [Display(Name = "Atividades a serem Trabalhadas")]
        [Required(ErrorMessage = "Obrigatório")]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        public string AtividadeTrabalhada { get; set; }

        [Display(Name = "Recursos Utilizados")]
        [Required(ErrorMessage = "Obrigatório")]
        public string RecursoUtilizado { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        public int BiografiaBasicaId { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        public int BiografiaComplementarId { get; set; }

        [Display(Name = "Data de Emissão")]
        [Required(ErrorMessage = "Obrigatório")]
        public DateTime DataEmissao { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Obrigatório")]
        public int StatusId { get; set; }
    }
}