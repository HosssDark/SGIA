using System;

namespace Site
{
    public class AtribuicaoViewModel
    {

    }

    public class AtribuicaoReportViewModel
    {
        public int StatusId { get; set; }
        public string Formato { get; set; } = "pdf";
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
    }
}