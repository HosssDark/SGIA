using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class Menu
    {
        [Key]
        public int MenuId { get; set; }
        public int? MenuPaiId { get; set; }
        public string Tipo { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Link { get; set; }
        public string Icone { get; set; }
        public bool Ativo { get; set; }
        public int Ordem { get; set; }
    }
}