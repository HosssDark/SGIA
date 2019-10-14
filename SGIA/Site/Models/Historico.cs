using System.Collections.Generic;

namespace Site.Models
{
    public class Historico
    {
        public string Versao { get; set; }
        public string Data { get; set; }
        public List<string> Descricao { get; set; } = new List<string>();

        public Historico(string Versao, string Data)
        {
            this.Versao = Versao;
            this.Data = Data;
        }
    }
}