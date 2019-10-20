using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class ParamDirectory
    {
        [Key]
        public int ParamId { get; set; }
        public string Directory { get; set; }
        public string Path { get; set; }
        public string PathVirtual { get; set; }
    }
}