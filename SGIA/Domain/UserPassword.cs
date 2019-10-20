using System;

namespace Domain
{
    public class UserPassword
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Guid { get; set; }
    }
}