using MusicPortal.Models;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<MusicContext>(options =>
    options.UseSqlServer(connection));

builder.Services.AddControllers();


builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MusicContext>();
    dbContext.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // ������� HTTP-������� (�������), �� ���� ����������
                      // ���� ����������� ������������ OpenAPI � ������ JSON.
                      // https://localhost:7140/openapi/v1.json

    app.MapScalarApiReference(); // ��������� ��� JSON-���� � ���� �� ���� �����
                                 // �������� ��������� ��������� Scalar
                                 // ��� �����䳿 � Web API-�������.
                                 // ��������� ��� ���������� Web API ��������� �� �������: https://localhost:7140/scalar/v1
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();