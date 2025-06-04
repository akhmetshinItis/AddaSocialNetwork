using System.Reflection;
using System.Text;
using Core.Abstractions;
using Core.Models;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class Entry
    {
        public static void AddCore(this IServiceCollection services)
        {
            services.AddMediator();
            services.AddServices();
            services.AddSignalR();
        }

        private static void AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserServiceIdentity, UserServiceIdentity>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<EmailServiceOptions>();
            
            services.AddScoped<IFriendsService, FriendsService>();
            services.AddScoped<IBusinessUserService, BusinessUserService>();
        }
    }
}