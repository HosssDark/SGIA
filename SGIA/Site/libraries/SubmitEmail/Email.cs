using EASendMail;

namespace Site.libraries.SubmitEmail
{
    public class Email
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


        public void SubmitEmailAsync(EmailSettings emailSettings, string ClientEmail, string Message)
        {
            SmtpMail oMail = new SmtpMail("TryIt");

            oMail.From = "brhosss@outlook.com";
            oMail.To = "brhosss@outlook.com";
            oMail.Subject = "test email from hotmail, outlook, office 365 account";
            oMail.TextBody = "this is a test email sent from c# project using hotmail.";
            SmtpServer oServer = new SmtpServer("smtp.live.com");
            oServer.User = "brhosss@outlook.com";
            oServer.Password = "hunter2525";
            oServer.Port = 587;
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
            SmtpClient oSmtp = new SmtpClient();
            oSmtp.SendMail(oServer, oMail);
        }
    }
}