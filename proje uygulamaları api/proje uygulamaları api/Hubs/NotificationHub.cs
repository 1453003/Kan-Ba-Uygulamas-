using Microsoft.AspNetCore.SignalR;
using proje_uygulamaları_api.Models;
using proje_uygulamaları_api.Dtos;
namespace proje_uygulamaları_api.Hubs
{
    public class NotificationHub : Hub
    {// Yeni hasta eklendiğinde istemcilere bildirim gönderen metot
        public async Task YeniHastaEklendi(string ad, string soyad, string kanGrubu)
        {
            // İstemcilere "YeniHastaEklendi" adında bir metot aracılığıyla bildirim gönderin
            await Clients.All.SendAsync("YeniHastaEklendi", ad, soyad, kanGrubu);
        }
    }
}
