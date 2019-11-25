using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Address
    {
        [Key]
        [Display(Name = "ID")]
        public int EnderecoId { get; set; }

        public int UserId { get; set; }

        [Display(Name = "Bairro")]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        public string Bairro { get; set; }

        [Display(Name = "Logradouro")]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        public string Logradouro { get; set; }

        [Display(Name = "Número")]
        [MaxLength(10, ErrorMessage = "Máximo de 10 caracteres")]
        public string Numero { get; set; }

        [Display(Name = "Cep")]
        [MaxLength(9, ErrorMessage = "Máximo de 9 caracteres")]
        public string Cep { get; set; }

        [Display(Name = "Estado")]
        public string UF { get; set; }

        [Display(Name = "Município")]
        public int MunicipioId { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }
    }
}