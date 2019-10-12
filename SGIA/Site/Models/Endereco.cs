using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class Endereco
    {
        [Key]
        public int EnderecoId { get; set; }
        public int MoradorId { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Cep { get; set; }
        public int EstadoId { get; set; }
        public int MunicipioId { get; set; }
        public int StatusId { get; set; }
    }
}