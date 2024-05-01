
using Microsoft.Extensions.Options;
using MimeKit;

using MailKit.Net.Smtp;
using MailKit.Security;
using SendMailDemo.Settings;

namespace SendMailDemo.Services
{
    public class SendMailServices : ISendMailServices
    {
        private readonly EmailSettings _emailsettings;

        public SendMailServices(IOptions<EmailSettings> mailsettings)
        {
            _emailsettings = mailsettings.Value;
        }
        public async Task<bool> SendMail( BodyDto body)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_emailsettings.Email),
                Subject = "Email from ksa crash lab contact form",
            };
            email.To.Add(MailboxAddress.Parse("elmotasembelahelsayed12@gmail.com"));
            var builder = new BodyBuilder();
           
                string bodyText = $"Name: {body.name} <br/> Email: {body.email} <br/> subject: {body.subject} <br/>  message: {body.message}";
                builder.HtmlBody=bodyText;
            
            
            email.Body = builder.ToMessageBody();
            email.From.Add(new MailboxAddress(_emailsettings.DisplayName, _emailsettings.Email));
            using var smtp = new SmtpClient();
            smtp.Connect(_emailsettings.Host, _emailsettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailsettings.Email, _emailsettings.Password);
            var result = await smtp.SendAsync(email);

            smtp.Disconnect(true);
            var status = result.Split(" ")[0];
            if (status == "2.0.0")
                return true;
            else
                return false;
        }
    }
}
