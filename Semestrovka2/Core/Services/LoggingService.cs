using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Core.Entities;

namespace Core.Services
{
    public interface ILoggingService
    {
        Task LogUserAction(string userId, string action, string details, LogLevel level = LogLevel.Information);
        Task LogError(string userId, string action, Exception exception, string details = null);
        Task LogSystemEvent(string eventName, string details, LogLevel level = LogLevel.Information);
    }

    public class LoggingService : ILoggingService
    {
        private readonly ILogger<LoggingService> _logger;
        private readonly IHostEnvironment _environment;

        public LoggingService(ILogger<LoggingService> logger, IHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
            _logger.LogInformation($"Current environment: {_environment.EnvironmentName}");
        }

        public async Task LogUserAction(string userId, string action, string details, LogLevel level = LogLevel.Information)
        {
            var logMessage = $"User {userId} performed action: {action}. Details: {details}";
            _logger.Log(level, logMessage);
            await Task.CompletedTask;
        }

        public async Task LogError(string userId, string action, Exception exception, string details = null)
        {
            var logMessage = $"Error occurred for user {userId} during action: {action}. Details: {details}";
            
            if (_environment.EnvironmentName.Equals("Development", StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogError(exception, logMessage);
            }
            else
            {
                _logger.LogError($"{logMessage}. Error: {exception.Message}");
            }
            
            await Task.CompletedTask;
        }

        public async Task LogSystemEvent(string eventName, string details, LogLevel level = LogLevel.Information)
        {
            var logMessage = $"System event: {eventName}. Details: {details}";
            _logger.Log(level, logMessage);
            await Task.CompletedTask;
        }
    }
} 