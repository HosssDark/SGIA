using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class Usuario
    {
        [Key]
        [Display(Name = "ID")]
        public int UsuarioId { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Profissão")]
        public string Profissao { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Display(Name = "Tipo")]
        public string Tipo { get; set; }

        [Display(Name = "Educação")]
        public string Educacao { get; set; }

        [Display(Name = "Habilidades")]
        public string Habilidades { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Lembre de Mim")]
        public bool LembreMim { get; set; }
    }
}