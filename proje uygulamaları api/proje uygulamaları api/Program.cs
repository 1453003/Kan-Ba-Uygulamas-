using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using proje_uygulamaları_api.Dtos;
using proje_uygulamaları_api.Efcore;
using proje_uygulamaları_api.Hubs;
using proje_uygulamaları_api.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("_myAllowSpecificOrigins",
                      builder =>
                      {
                          builder.WithOrigins("https://localhost:7193")
                                 .AllowAnyHeader()
                                 .AllowAnyMethod()
                                 .AllowCredentials();
                      });
});

// Add SignalR service
builder.Services.AddSignalR();

// Add DbContext with configuration
builder.Services.AddDbContext<HastaOtomasyonuEklemeDbContext>(options =>
    options.UseSqlServer("Server=DESKTOP-7915U5S\\ROJINA;Database=HastaOtomasyon;User Id=hasta;Password=00000;TrustServerCertificate=true"));

// Add scoped services for dependency injection
builder.Services.AddScoped<IHastaServices, HastaServices>();
builder.Services.AddScoped<IkullanıcıServices, KullanıcıServices>(); // KullanıcıServices sınıfını DI konteynerine ekliyoruz.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("_myAllowSpecificOrigins");

// Hasta routes
app.MapGet("/hasta", (IHastaServices hastaServices) =>
{
    return hastaServices.GetTumHastalar();
});

app.MapPost("/hasta", async (HastaOlusturDto hasta, IHastaServices hastaService, IHubContext<NotificationHub> hubContext) =>
{
    hastaService.HastaEkle(hasta);
    // Yeni hasta eklendiğinde bildirim gönder
    try
    {
        await hubContext.Clients.All.SendAsync("YeniHastaEklendi", hasta.Ad);
        Console.WriteLine("Bildirim gönderildi: " + hasta.Ad);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Bildirim gönderme hatası: " + ex.Message);
    }
});

app.MapPut("/hasta", (HastaGuncelleDto hasta, IHastaServices hastaServices) =>
{
    hastaServices.HastaGuncelle(hasta);
});

app.MapDelete("/hasta", (int id, IHastaServices hastaService) =>
{
    hastaService.HastaSil(id);
});

// Kullanıcı routes
app.MapGet("/kullanıcı", (IkullanıcıServices kullanıcıServices) =>
{
    return kullanıcıServices.GetTumKullanıcılar();
});

app.MapPost("/kullanıcı", (KullanıcıOlusturDto kullanıcı, IkullanıcıServices kullanıcıService) =>
{
    kullanıcıService.KullanıcıEkle(kullanıcı);
});

app.MapPut("/kullanıcı", (kullanıcıGuncelleDto kullanıcı, IkullanıcıServices kullanıcıService) =>
{
    kullanıcıService.KullanıcıGüncelle(kullanıcı);
});

app.MapDelete("/kullanıcı", (int id, IkullanıcıServices kullanıcıService) =>
{
    kullanıcıService.KullanıcıSil(id);
});

// Map SignalR hub
app.MapHub<NotificationHub>("/notificationHub");

app.Run();
