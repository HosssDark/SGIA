using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }
        public string Descricao { get; set; }
    }
}