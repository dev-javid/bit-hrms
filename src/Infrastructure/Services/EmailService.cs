using System.Net.Mail;
using static Infrastructure.Services.EmailService;

namespace Infrastructure.Services
{
    internal class EmailService(EmailConfiguration emailConfiguration) : IEmailService
    {
        public void Send(Email reciever, string subject, string htmlBody)
        {
            if (emailConfiguration.Enabled)
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(reciever.Value);
                mail.From = new MailAddress(emailConfiguration.UserName);
                mail.Subject = subject;
                mail.Body = htmlBody;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new()
                {
                    Port = emailConfiguration.Port,
                    EnableSsl = emailConfiguration.EnableSsl,
                    UseDefaultCredentials = false,
                    Host = emailConfiguration.Host,
                    Credentials = new System.Net.NetworkCredential(emailConfiguration.UserName, emailConfiguration.Password)
                };

                smtp.Send(mail);
            }
            else
            {
                Serilog.Log.Warning("EMAIL SENT: {@Address} {@HtmlBody} ", reciever.Value, htmlBody);
            }
        }

        internal class EmailConfiguration
        {
            public int Port { get; set; }

            public bool EnableSsl { get; set; }

            public string Host { get; set; } = null!;

            public string UserName { get; set; } = null!;

            public string Password { get; set; } = null!;

            public bool Enabled { get; set; }
        }
    }
}
