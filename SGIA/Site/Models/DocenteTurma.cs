using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class DocenteTurma
    {
        [Key]
        public int DocenteTurmaId { get; set; }
        public int DocenteId { get; set; }
        public int TurmaId { get; set; }
    }
}