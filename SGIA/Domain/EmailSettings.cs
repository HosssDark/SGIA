namespace Domain
{
    public class EmailSettings
    {
        public string PrimaryDomain { get; set; } = "smtp.gmail.com";
        public int PrimaryPort { get; set; } = 587;
        public string UsernameEmail { get; set; } = "brhosss@gmail.com";
        public string UsernamePassword { get; set; } = "hunter2525";
        public string FromEmail { get; set; } = "brhosss@gmail.com";
        public bool Ssl { get; set; } = false;
    }
}