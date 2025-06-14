using Core;
using Core.Hubs;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using Persistence;
using S3;
using Web.Authentication;
using Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddCore();
builder.Services.AddUserContext();
builder.Services.AddS3Storage(configuration.GetSection("Application:S3").Get<S3Options>());
builder.Services.AddSignalR();

// Add logging service as singleton
builder.Services.AddSingleton<ILoggingService, LoggingService>();

// Добавление Identity
builder.Services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    // для токена сброса пароля
    .AddDefaultTokenProviders();

// Для редиректа неавторизованных пользователей
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/signup";
    options.AccessDeniedPath = "/Error/Forbidden";
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// почистить
app.MapHub<ChatHub>("/chat");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Обработка 404 ошибок
app.UseStatusCodePages(async context =>
{
    if (context.HttpContext.Response.StatusCode == 404)
    {
        var path = context.HttpContext.Request.Path.Value?.StartsWith("/admin") == true 
            ? "/admin/Error/NotFound" 
            : "/Error/NotFound";
        context.HttpContext.Response.Redirect(path);
    }
});

app.UseMiddleware<RequestLoggingMiddleware>();

app.MapStaticAssets();

    app.MapControllerRoute(
        "Admin",
        "{area:exists}/{controller=Home}/{action=Index}/{id?}")
        .WithStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();