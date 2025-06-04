using Core.Abstractions;

namespace Web.Authentication
{
    public static class Entry
    {
        public static IServiceCollection AddUserContext(this IServiceCollection services) =>
            services
                .AddScoped<IUserContext, UserContext>();
    }
}