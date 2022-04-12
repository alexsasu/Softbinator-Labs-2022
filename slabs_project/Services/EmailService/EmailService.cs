using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;
using slabs_project.Models.Entities;

namespace slabs_project.Services.EmailService
{
    public class EmailService : IEmailService
    {
        public void Send(EmailDetails emailDetails)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(emailDetails.FromName, emailDetails.FromEmail));
            email.To.Add(new MailboxAddress(emailDetails.ToName, emailDetails.ToEmail));
            email.Subject = "test email 2";
            email.Body = new TextPart("plain")
            {
                Text = "content test email 2"
            };

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect("smtp.gmail.com", 465, true);
                smtpClient.Authenticate(emailDetails.FromEmail, "introdu o parola");
                smtpClient.Send(email);
                smtpClient.Disconnect(true);
                smtpClient.Dispose();
            }
        }
    }
}
