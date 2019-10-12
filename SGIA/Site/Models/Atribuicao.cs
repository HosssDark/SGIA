using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class Atribuicao
    {
        [Key]
        public int AtribuicaoId { get; set; }
        public DateTime DataCadastro { get; set; }
        public int DiciplinaId { get; set; }
        public int ProjetoId { get; set; }
        public int DocenteID { get; set; }
        public int StatusId { get; set; }
    }
}