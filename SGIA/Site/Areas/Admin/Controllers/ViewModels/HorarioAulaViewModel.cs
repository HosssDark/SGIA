using Domain;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Site
{
    public class HorarioAulaViewModel
    {
        public HorarioAula HorarioAula { get; set; }
        public string Image { get; set; }

        [Display(Name = "Imagem (700x500)")]
        public IFormFile File { get; set; }
    }

    public class HorarioAulaReportViewModel
    {
  
    }
}