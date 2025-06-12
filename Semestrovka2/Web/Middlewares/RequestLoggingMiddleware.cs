using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Core.Services;
using System.Security.Claims;
using System.Linq;

namespace Web.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        private readonly ILoggingService _loggingService;

        public RequestLoggingMiddleware(
            RequestDelegate next,
            ILogger<RequestLoggingMiddleware> logger,
            ILoggingService loggingService)
        {
            _next = next;
            _logger = logger;
            _loggingService = loggingService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                string userId = "Anonymous";
                if (context.User.Identity?.IsAuthenticated == true)
                {
                    userId = context.User.Claims
                        .FirstOrDefault(x => x.Type == ClaimTypes.Authentication)?.Value ?? "Unknown";
                }

                var requestPath = context.Request.Path;
                var requestMethod = context.Request.Method;

                await _loggingService.LogUserAction(
                    userId,
                    "HTTP Request",
                    $"Method: {requestMethod}, Path: {requestPath}");

                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request");
                throw;
            }
        }
    }
} 