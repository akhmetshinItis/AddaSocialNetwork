using Core;
using Core.Hubs;
using Microsoft.AspNetCore.Identity;
using Persistence;
using Persistence.Extensions;
using Web.Authentication;
using Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllersWithViews();
    builder.Services.AddPersistenceLayer(builder.Configuration);
    builder.Services.AddCore();
    builder.Services.AddUserContext();
    
    builder.Services.AddSignalR();

    // Добавление Identity
    builder.Services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        // для токена сброса пароля
        .AddDefaultTokenProviders();

    // Для редиректа неавторизованных пользователей
    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.LoginPath = "/signup";
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
    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapStaticAssets();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
        .WithStaticAssets();


    app.Run();