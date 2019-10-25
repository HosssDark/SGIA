using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Semestre
    {
        [Display(Name = "ID")]
        public int SemestreId { get; set; }

        [Display(Name = "Turma")]
        [Required(ErrorMessage = "Obrigatório")]
        public int TurmaId { get; set; }

        [Display(Name = "Diciplina")]
        [Required(ErrorMessage = "Obrigatório")]
        public int DiciplinaId { get; set; }

        [Display(Name = "Docente")]
        [Required(ErrorMessage = "Obrigatório")]
        public int DocenteId { get; set; }

        [Display(Name = "Carga Horária")]
        [Required(ErrorMessage = "Obrigatório")]
        public int CargaHoraria { get; set; }

        [Display(Name = "Crédito")]
        [Required(ErrorMessage = "Obrigatório")]
        public double Credito { get; set; }
    }
}