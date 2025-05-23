using Core;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Persistence;
using Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllersWithViews();
    builder.Services.AddPersistenceLayer(builder.Configuration);
    builder.Services.AddCore();

    // Добавление Identity
    builder.Services.AddIdentity<User, IdentityRole<Guid>>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        // для токена сброса пароля
        .AddDefaultTokenProviders();

    // Для редиректа неавторизованных пользователей
    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.LoginPath = "/signup";
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

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