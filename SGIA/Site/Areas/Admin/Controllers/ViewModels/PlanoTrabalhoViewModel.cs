using Domain;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Site
{
    public class PlanoTrabalhoViewModel
    {
        public PlanoTrabalho PlanoTrabalho { get; set; }
        public string Image { get; set; }

        [Display(Name = "Imagem")]
        public IFormFile File { get; set; }
    }

    public class PlanoTrabalhoReportViewModel
    {

    }
}