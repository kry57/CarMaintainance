using CarMaintenance.Application.Services;
using CarMaintenance.Infrastructre.OptionsPattern;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;


namespace CarMaintenance.Infrastructre.Implementations
{
    public class EmailService(IOptions<MailSettings> options) : IEmailService
    {
        private readonly MailSettings _settings = options.Value;

        public async Task SendAsync(string to, string subject, string htmlBody)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse("marcelle.schowalter65@ethereal.email"));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            message.Body = new BodyBuilder { HtmlBody = htmlBody }.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("marcelle.schowalter65@ethereal.email", "3CXxCXt993arJKD3j5");
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }
    }
}
