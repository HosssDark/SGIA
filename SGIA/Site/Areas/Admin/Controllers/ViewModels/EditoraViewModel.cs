using Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Site
{
    public class EditoraViewModel
    {
        public Editora Editora { get; set; }
        public string Image { get; set; }

        [Display(Name = "Imagem")]
        public IFormFile File { get; set; }
    }

    public class EditoraReportViewModel
    {
        public int EditoraId { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Nome { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
    }
}