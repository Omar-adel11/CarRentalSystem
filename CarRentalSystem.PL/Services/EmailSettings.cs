using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;

namespace CarRentalSystem.PL.Services
{
    public class EmailSettings : IEmailSender
    {
        private readonly IConfiguration _config;

        public EmailSettings(IConfiguration config)
        {
            _config = config;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mailSettings = _config.GetSection("MailSettings");
            var senderEmail = mailSettings["Email"];
            var senderPassword = mailSettings["Password"];
            var senderDisplayName = mailSettings["DisplayName"];
            var host = mailSettings["Host"];
            var port = int.Parse(mailSettings["Port"]);

            try
            {
                using (var client = new SmtpClient(host, port))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(senderEmail, senderPassword);

                    var mail = new MailMessage
                    {
                        From = new MailAddress(senderEmail, senderDisplayName),
                        Subject = subject,
                        Body = htmlMessage,
                        IsBodyHtml = true
                    };

                    mail.To.Add(email);

                    client.Send(mail);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email sending failed: {ex.Message}");
            }

            return Task.CompletedTask;
        }
    }
}
