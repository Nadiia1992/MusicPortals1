using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;
using MusicPortal.Repository;
using MusicPortal.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.Name = "SessionCookie";
});

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MusicContext>(options => options.UseSqlServer(connection));


builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRepositoryUser, UserRepository>();
builder.Services.AddScoped<IRepositorySong, SongRepository>();
builder.Services.AddScoped<PasswordHasher>();


var app = builder.Build();
app.UseStaticFiles();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();