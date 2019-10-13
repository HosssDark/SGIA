using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class DocenteTurma
    {
        [Key]
        [Display(Name = "ID")]
        public int DocenteTurmaId { get; set; }

        [Display(Name = "Docente")]
        [Required]
        public int DocenteId { get; set; }

        [Display(Name = "Turma")]
        [Required]
        public int TurmaId { get; set; }
    }
}