using Domain;
using System.Collections.Generic;

namespace Site.Areas.Admin.Controllers.ViewModels
{
    public class TurmaViewModel
    {

    }

    public class TurmaDetalhesViewModel
    {
        public int TormaId { get; set; }
        public string Name { get; set; }
        public string Descricao { get; set; }
        public int QtdeSemestres { get; set; }
        public List<TurmaSemestreViewModel> Semestres { get; set; }
        public int Duracao { get; set; }
        public int CoordenadorId { get; set; }
        public TurmaCoordenadorViewModel Coordenador { get; set; }
        public string Image { get; set; }
    }

    public class TurmaSemestreViewModel
    {
        public int SemestreId { get; set; }
        public int TurmaId { get; set; }
        public int DiciplinaId { get; set; }
        public string Diciplina { get; set; }
        public int DocenteId { get; set; }
        public string Docente { get; set; }
        public int CargaHoraria { get; set; }
        public double Credito { get; set; }
    }

    public class TurmaCoordenadorViewModel
    {
        public int CoordenadorId { get; set; }
        public string Name { get; set; }
        public int CargoId { get; set; }
        public string Cargo { get; set; }
    }
}