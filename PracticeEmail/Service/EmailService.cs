using Microsoft.Extensions.Options;
using MimeKit;
using PracticeEmail.Helpper;
using MailKit.Net.Smtp;
using System.Net.Mail;
using MailKit.Security;

namespace PracticeEmail.Service
{
    public class EmailService : IEmailServices
    {
        private readonly EmailSettings _emailSettings
            ;
        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }
        public async Task SendEmailAsync(MailRequest request)
        {
            var Email = new MimeMessage();
            Email.Sender = MailboxAddress.Parse(_emailSettings.Email);
            Email.To.Add(MailboxAddress.Parse(request.ToEmail));
            Email.Subject = request.Subject;

            var builder = new BodyBuilder();

            builder.HtmlBody = request.Body;

            Email.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            smtp.Connect(_emailSettings.Host ,_emailSettings.Port ,SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.Email, _emailSettings.Password);
            await smtp.SendAsync(Email);
            smtp.Disconnect(true);

        }
    }
}
