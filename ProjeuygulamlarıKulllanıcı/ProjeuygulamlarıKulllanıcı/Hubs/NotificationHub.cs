using Microsoft.AspNetCore.SignalR;

namespace ProjeuygulamlarıKulllanıcı.Hubs
{
    public class NotificationHub : Hub
    {
        // Yeni hasta eklendiğinde istemcilere bildirim gönderen metot
        public async Task YeniHastaEklendi(string ad, string soyad, string kanGrubu)
        {
            // İstemcilere "YeniHastaEklendi" adında bir metot aracılığıyla bildirim gönderin
            await Clients.All.SendAsync("YeniHastaEklendi", ad, soyad, kanGrubu);
        }
    }
}
