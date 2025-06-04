using Core.Abstractions;
using Core.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace Core.Services ;

    public class EmailService : IEmailService
    {
        private readonly EmailServiceOptions _options;

        public EmailService(EmailServiceOptions options)
        {
            _options = options;
        }

        public async Task SendEmailAsync(string email, string subject, string content)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_options.Name, _options.Email));
            message.To.Add(new MailboxAddress(email, email));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = content
            };

            await SendMessage(message);
        }
        
        public async Task SendMessage(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                client.CheckCertificateRevocation = _options.CheckCertificateRevocation;
                await client.ConnectAsync(_options.Host, _options.Port, true);
                await client.AuthenticateAsync(_options.Email, _options.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            
        }
    }