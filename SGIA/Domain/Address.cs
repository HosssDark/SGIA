using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Address
    {
        [Key]
        [Display(Name = "ID")]
        public int EnderecoId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "Obrigatório")]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        public string Bairro { get; set; }

        [Display(Name = "Logradouro")]
        [Required(ErrorMessage = "Obrigatório")]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        public string Logradouro { get; set; }

        [Display(Name = "Número")]
        [Required(ErrorMessage = "Obrigatório")]
        [MaxLength(10, ErrorMessage = "Máximo de 10 caracteres")]
        public string Numero { get; set; }

        [Display(Name = "Cep")]
        [Required(ErrorMessage = "Obrigatório")]
        [MaxLength(9, ErrorMessage = "Máximo de 9 caracteres")]
        public string Cep { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Obrigatório")]
        public int EstadoId { get; set; }

        [Display(Name = "Município")]
        [Required(ErrorMessage = "Obrigatório")]
        public int MunicipioId { get; set; }
    }
}