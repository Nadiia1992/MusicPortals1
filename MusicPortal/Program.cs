using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using MusicPortal;
using MusicPortal.Models;
using MusicPortal.Repository;
using MusicPortal.Services;
using System.Globalization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.Name = "SessionCookie";
});

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MusicContext>(options => options.UseSqlServer(connection));

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");


builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options => {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(Resource));
    });

var supportedCultures = new[]
{
    new CultureInfo("uk"),
    new CultureInfo("en"),
    new CultureInfo("de"),
    new CultureInfo("fr")
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("uk");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});


builder.Services.AddScoped<IRepositoryUser, UserRepository>();
builder.Services.AddScoped<IRepositorySong, SongRepository>();
builder.Services.AddScoped<IRepositoryGenre, GenreRepository>();
builder.Services.AddScoped<IRepositorySinger, SingerRepository>();
builder.Services.AddScoped<PasswordHasher>();


var app = builder.Build();
app.UseRequestLocalization();
app.UseStaticFiles();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();