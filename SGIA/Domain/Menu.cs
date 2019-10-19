using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Menu
    {
        [Key]
        [Display(Name = "ID")]
        public int MenuId { get; set; }

        [Display(Name = "Menu Pai")]
        public int? MenuPaiId { get; set; }

        [Display(Name = "Tipo")]
        public string Tipo { get; set; }

        [Display(Name = "Título")]
        public string Titulo { get; set; }
        
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Link")]
        public string Link { get; set; }

        [Display(Name = "Icone")]
        public string Icone { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        [Display(Name = "Ordem")]
        public int Ordem { get; set; }

        [Display(Name = "Tipo Acesso")]
        public int TipoAcesso { get; set; }

        [Display(Name = "Painel")]
        public string Painel { get; set; }
    }
}