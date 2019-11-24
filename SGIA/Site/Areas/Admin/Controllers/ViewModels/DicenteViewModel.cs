using Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Site
{
    public class DicenteViewModel
    {
        public Dicente Dicente { get; set; }
        public string Image { get; set; }

        [Display(Name = "Imagem")]
        public IFormFile File { get; set; }
    }

    public class DicenteReportViewModel
    {
        public int StatusId { get; set; }
        public string Formato { get; set; } = "pdf";
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
    }
}