using Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Site
{
    public class ProjetoViewModel
    {
        public Projeto Projeto { get; set; }
        public string Image { get; set; }

        [Display(Name = "Imagem (700x500)")]
        public IFormFile File { get; set; }
    }

    public class ProjetoReportViewModel
    {
        public int DocenteId { get; set; }
        public int StatusId { get; set; }
        public string Formato { get; set; } = "pdf";
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
    }
}