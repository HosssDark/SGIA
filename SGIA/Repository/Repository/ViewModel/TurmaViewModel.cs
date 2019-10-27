using Domain;

namespace Repository.Repository.ViewModel
{
    public class TurmaViewModel
    {
        public int TormaId { get; set; }
        public string Name { get; set; }
        public string Descricao { get; set; }
        public int QtdeSemestres { get; set; }
        public int QtdeEstudantes { get; set; }
        public int Duracao { get; set; }
        public int CoordenadorId { get; set; }
        public User Coordenador { get; set; }
        public string Image { get; set; }
    }
}