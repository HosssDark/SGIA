using Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Site
{
    public class PlanoEnsinoViewModel
    {
        public PlanoEnsino PlanoEnsino { get; set; }
        public string Image { get; set; }

        [Display(Name = "Imagem (700x500)")]
        public IFormFile File { get; set; }
    }

    public class PlanoEnsinoReportViewModel
    {
        public int DiciplinaId { get; set; }
        public int StatusId { get; set; }
        public string Formato { get; set; } = "pdf";
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
    }
}