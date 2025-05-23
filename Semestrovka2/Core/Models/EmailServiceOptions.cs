using Microsoft.Extensions.Configuration;

namespace Core.Models
{
    public class EmailServiceOptions
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool CheckCertificateRevocation { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

        public EmailServiceOptions(IConfiguration configuration)
        {
            var settings = configuration.GetSection("EmailSettings");
            Email = settings["Email"] ?? throw new Exception($"Email configuration is missing in appsettings.json");
            Password = settings["Password"] ?? throw new Exception($"Email configuration is missing in appsettings.json");
            Name = settings["Name"] ?? throw new Exception($"Email configuration is missing in appsettings.json");
            CheckCertificateRevocation = bool.Parse(settings["CheckCertificateRevocation"] ?? string.Empty);
            Host = settings["Host"] ?? throw new Exception($"Email configuration is missing in appsettings.json");
            Port = Convert.ToInt32(settings["Port"]);
        }
    }
}