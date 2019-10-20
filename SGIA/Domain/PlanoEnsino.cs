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
        [Required]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Diciplina")]
        [Required]
        public int DiciplinaId { get; set; }

        [Display(Name = "Docente")]
        [Required]
        public int UserId { get; set; }

        [Display(Name = "Turma")]
        [Required]
        public int TurmaId { get; set; }

        [Display(Name = "Objetivo da Área")]
        [Required]
        public string ObjetivoArea { get; set; }

        [Display(Name = "Objetivo Geral")]
        [Required]
        public string ObjetivoGeral { get; set; }

        [Display(Name = "Ementa")]
        [Required]
        public int EmentaId { get; set; }

        [Display(Name = "Especificação do Conteúdo Programático")]
        [Required]
        public string EspecificacaoConteudo { get; set; }

        [Display(Name = "Metodologia para Avaliação")]
        [Required]
        public string MetodologiaAvaliacao { get; set; }

        [Display(Name = "Técnica Pedagógica")]
        [Required]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        public string TecnicaPedagogica { get; set; }

        [Display(Name = "Atividades a serem Trabalhadas")]
        [Required]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        public string AtividadeTrabalhada { get; set; }

        [Display(Name = "Recursos Utilizados")]
        [Required]
        public string RecursoUtilizado { get; set; }

        [Required]
        public int BiografiaBasicaId { get; set; }

        [Required]
        public int BiografiaComplementarId { get; set; }

        [Display(Name = "Data de Emissão")]
        [Required]
        public DateTime DataEmissao { get; set; }

        [Display(Name = "Status")]
        [Required]
        public int StatusId { get; set; }
    }
}