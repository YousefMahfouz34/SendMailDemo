
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
        public async Task SendMail(string mailto, string subject, string body)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_emailsettings.Email),
                Subject = subject,
            };
            email.To.Add(MailboxAddress.Parse(mailto));
            var builder = new BodyBuilder();
            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();
            email.From.Add(new MailboxAddress(_emailsettings.DisplayName, _emailsettings.Email));
            using var smtp = new SmtpClient();
            smtp.Connect(_emailsettings.Host, _emailsettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailsettings.Email, _emailsettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
