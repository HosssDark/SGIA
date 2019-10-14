using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class Endereco
    {
        [Key]
        [Display(Name = "ID")]
        public int EnderecoId { get; set; }

        [Required]
        public int MoradorId { get; set; }

        [Display(Name = "Bairro")]
        [Required]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        public string Bairro { get; set; }

        [Display(Name = "Logradouro")]
        [Required]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        public string Logradouro { get; set; }

        [Display(Name = "Número")]
        [Required]
        [MaxLength(10, ErrorMessage = "Máximo de 10 caracteres")]
        public string Numero { get; set; }

        [Display(Name = "Cep")]
        [Required]
        [MaxLength(9, ErrorMessage = "Máximo de 9 caracteres")]
        public string Cep { get; set; }

        [Display(Name = "Estado")]
        [Required]
        public int EstadoId { get; set; }

        [Display(Name = "Município")]
        [Required]
        public int MunicipioId { get; set; }

        [Display(Name = "Status")]
        [Required]
        public int StatusId { get; set; }
    }
}