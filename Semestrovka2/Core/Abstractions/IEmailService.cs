using MimeKit;

namespace Core.Abstractions ;

    public interface IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string content);
        
        public Task SendMessage(MimeMessage message);
    }