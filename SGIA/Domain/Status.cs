using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Status
    {
        [Key]
        [Display(Name = "ID")]
        public int StatusId { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}