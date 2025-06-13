using System.Net;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using Core.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IHostEnvironment _environment;
    private readonly IServiceProvider _serviceProvider;

    public ExceptionHandlingMiddleware(
        RequestDelegate next, 
        ILogger<ExceptionHandlingMiddleware> logger,
        IHostEnvironment environment,
        IServiceProvider serviceProvider)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
        _serviceProvider = serviceProvider;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await ProcessExceptionAsync(context, ex);
        }
    }
    
    private async Task ProcessExceptionAsync(HttpContext context, Exception exception)
    {
        switch (exception)
        {
            case UnauthorizedAccessException accessException:
                await HandleUnauthorizedAccessExceptionAsync(context, accessException);
                break;
            case NotFoundException notFoundException:
                await HandleEntityNotFoundExceptionAsync(context, notFoundException);
                break;
            case ApplicationExceptionBase applicationException:
                await HandleApplicationExceptionBaseAsync(context, applicationException);
                break;
            case OutOfMemoryException outOfMemoryException:
                await HandleOutOfMemoryExceptionAsync(context, outOfMemoryException);
                break;
            default:
                await HandleExceptionAsync(context, exception);
                break;
        }
    }
    
    private async Task LogAndReturnAsync(
        HttpContext context,
        Exception exception,
        string errorText,
        HttpStatusCode responseCode,
        LogLevel logLevel = LogLevel.Error,
        Dictionary<string, object>? details = null)
    {
        var errorId = Guid.NewGuid().ToString();
        details ??= new Dictionary<string, object>();
        details.Add("errorId", errorId);

        if (_environment.IsDevelopment())
        {
            _logger.Log(logLevel, exception, "Error #{errorId}: {errorText}", errorId, errorText);
        }
        else
        {
            _logger.Log(logLevel, "Error #{errorId}: {errorText}. Message: {message}", 
                errorId, errorText, exception.Message);
        }

        // Redirect to custom error pages for 404 and 403
        if (responseCode == HttpStatusCode.NotFound)
        {
            var path = context.Request.Path.Value?.StartsWith("/admin") == true 
                ? "/admin/Error/NotFound" 
                : "/Error/NotFound";
            context.Response.Redirect(path);
            return;
        }
        
        if (responseCode == HttpStatusCode.Forbidden)
        {
            var path = context.Request.Path.Value?.StartsWith("/admin") == true 
                ? "/admin/Error/Forbidden" 
                : "/Error/Forbidden";
            context.Response.Redirect(path);
            return;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)responseCode;

        var response = new ProblemDetails
        {
            Title = errorText,
            Instance = null,
            Status = (int)responseCode,
            Type = null,
        };

        foreach (var detail in details)
            response.Extensions.Add(detail.Key, detail.Value);

        var jsonOptions = context.RequestServices.GetRequiredService<IOptions<JsonOptions>>();
       
        var problemDetails = new ProblemDetails
        {
            Title = "error",
            Detail = exception.Message,
            Status = (int)responseCode,
            Type = exception.GetType().FullName,
        };
        
        await context.Response.WriteAsJsonAsync(problemDetails);
    }
    
    /// <summary>
    /// Обработка исключения <see cref="Exception"/>
    /// </summary>
    /// <param name="context">Контекст запроса ASP.NET</param>
    /// <param name="exception">Исключение</param>
    /// <returns>Задача на обработку запроса ASP.NET</returns>
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var errorText = exception.Message;
        var logLevel = LogLevel.Error;
        var responseCode = System.Net.HttpStatusCode.InternalServerError;

        var errorPath = context.Request.Path.Value;
        var isServerErrorPage = errorPath != null && 
            (errorPath.Contains("/Error/ServerError") || errorPath.Contains("/admin/Error/ServerError"));

        if (isServerErrorPage)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync("Internal Server Error");
            return;
        }

        using (var scope = _serviceProvider.CreateScope())
        {
            var emailService = scope.ServiceProvider.GetRequiredService<Core.Abstractions.IEmailService>();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var adminEmail = configuration["EmailSettings:Email"];
            if (!string.IsNullOrEmpty(adminEmail))
            {
                var subject = "Ошибка 500 на сервере";
                var content = $"Ошибка: {exception.Message}\n\nStackTrace:\n{exception.StackTrace}";
                try { await emailService.SendEmailAsync(adminEmail, subject, content); } catch { /* ignore */ }
            }
        }

        var path = errorPath?.StartsWith("/admin") == true 
            ? "/admin/Error/ServerError" 
            : "/Error/ServerError";
        context.Response.Redirect(path);
    }
    
    /// <summary>
    /// Обработка исключения <see cref="UnauthorizedAccessException"/>
    /// </summary>
    /// <param name="context">Контекст запроса ASP.NET</param>
    /// <param name="exception">Исключение</param>
    /// <returns>Задача на обработку запроса ASP.NET</returns>
    private async Task HandleUnauthorizedAccessExceptionAsync(HttpContext context, UnauthorizedAccessException exception)
    {
        var errorText = "Доступ к запрашиваемому ресурсу ограничен";
        var logLevel = LogLevel.Warning;
        var responseCode = HttpStatusCode.Forbidden;
        await LogAndReturnAsync(context, exception, errorText, responseCode, logLevel);
    }

    /// <summary>
    /// Обработка исключения <see cref="NotFoundException"/>
    /// </summary>
    /// <param name="context">Контекст запроса ASP.NET</param>
    /// <param name="exception">Исключение</param>
    /// <returns>Задача на обработку запроса ASP.NET</returns>
    private async Task HandleEntityNotFoundExceptionAsync(HttpContext context,
        NotFoundException exception)
    {
        var errorText = exception.Message;
        var logLevel = LogLevel.Warning;
        var responseCode = HttpStatusCode.NotFound;
        await LogAndReturnAsync(context, exception, errorText, responseCode, logLevel);
    }
    
    /// <summary>
    /// Обработка исключения <see cref="ApplicationExceptionBase"/>
    /// </summary>
    /// <param name="context">Контекст запроса ASP.NET</param>
    /// <param name="exception">Исключение</param>
    /// <returns>Задача на обработку запроса ASP.NET</returns>
    private async Task HandleApplicationExceptionBaseAsync(HttpContext context, ApplicationExceptionBase exception)
    {
        var errorText = exception.Message;
        var logLevel = LogLevel.Warning;
        var responseCode = HttpStatusCode.BadRequest;
        await LogAndReturnAsync(context, exception, errorText, responseCode, logLevel);
    }
    
    /// <summary>
    /// Обработка исключения <see cref="OutOfMemoryException"/>
    /// </summary>
    /// <param name="context">Контекст запроса ASP.NET</param>
    /// <param name="exception">Исключение</param>
    /// <returns>Задача на обработку запроса ASP.NET</returns>
    private async Task HandleOutOfMemoryExceptionAsync(HttpContext context, OutOfMemoryException exception)
    {
        var errorText = $"Server has troubles with its infrastructure. Please inform system administrator about this issue.";
        var logLevel = LogLevel.Error;
        var responseCode = HttpStatusCode.InternalServerError;
        await LogAndReturnAsync(context, exception, errorText, responseCode, logLevel);
    }
}