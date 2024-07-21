using Microsoft.AspNetCore.SignalR.Client;
using ProjeuygulamlarıKulllanıcı.Dtos;
using Microsoft.Extensions.Logging;
using ProjeuygulamlarıKulllanıcı.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjeuygulamlarıKulllanıcı.Services
{
    public abstract class BaseService
    {
        protected readonly HttpClient _client;
        protected readonly JsonSerializerOptions _serializerOptions;

        public BaseService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }
    }
    public class SignalRService
    {
        private readonly HubConnection _hubConnection;

        public SignalRService()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7193/notificationHub")
                .ConfigureLogging(logging =>
                {
                    logging.SetMinimumLevel(LogLevel.Debug);
                    logging.AddConsole();
                })
                .Build();

            ConnectToHub();
        }

        private async void ConnectToHub()
        {
            try
            {
                await _hubConnection.StartAsync();
                Console.WriteLine("SignalR bağlantısı başarılı.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SignalR bağlantısı başarısız: {ex.Message}");
            }

            _hubConnection.Closed += async (error) =>
            {
                // Bağlantı kapandığında tekrar başlat
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _hubConnection.StartAsync();
            };
        }

        public void ListenToHub(Action<string> messageHandler)
        {
            _hubConnection.On<string>("YeniHastaEklendi", message =>
            {
                messageHandler?.Invoke(message);
            });
        }
    }

    public class HttpsClientHandlerService
    {
        public HttpMessageHandler GetPlatformMessageHandler()
        {
#if ANDROID
            var handler = new Xamarin.Android.Net.AndroidMessageHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert != null && cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
#elif IOS
            var handler = new NSUrlSessionHandler
            {
                TrustOverrideForUrl = IsHttpsLocalhost
            };
            return handler;
#else
            throw new PlatformNotSupportedException("Only Android and iOS supported.");
#endif
        }

#if IOS
        public bool IsHttpsLocalhost(NSUrlSessionHandler sender, string url, Security.SecTrust trust)
        {
            return url.StartsWith("https://localhost");
        }
#endif
    }

    public static class UrlHelper
    {
        private static readonly string BaseUrl = "https://localhost:7193";
        public static readonly string HastaUrl = $"{BaseUrl}/Hasta";
        public static readonly string KullanıcıUrl = $"{BaseUrl}/Kullanıcı";
    }

    public class HastaServices : BaseService, IHastaServices
    {
        async Task<List<Hasta>> IHastaServices.GetTumHastalar()
        {
            Uri uri = new Uri(UrlHelper.HastaUrl);
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var sonuc = JsonSerializer.Deserialize<List<Hasta>>(content, _serializerOptions);
                return sonuc;
            }
            return new List<Hasta>();
        }

        public async Task HastaEkle(HastaOlusturDto hasta)
        {
            var jsonContent = JsonContent.Create(hasta, options: _serializerOptions);
            HttpResponseMessage response = await _client.PostAsync(UrlHelper.HastaUrl, jsonContent);
            if (response.IsSuccessStatusCode)
            {
                // Handle success
            }
        }

        public async Task HastaGuncelle(HastaGuncelleDto hasta)
        {
            var jsonContent = JsonContent.Create(hasta, options: _serializerOptions);
            HttpResponseMessage response = await _client.PutAsync(UrlHelper.HastaUrl, jsonContent);
            if (response.IsSuccessStatusCode)
            {
                // Handle success
            }
        }

        public async Task HastaSil(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"{UrlHelper.HastaUrl}?id={id}");
            if (response.IsSuccessStatusCode)
            {
                // Handle success
            }
        }
    }

    public class KullanıcıServices : BaseService, IKullanıcıServices
    {
        public async Task<List<Kullanıcı>> GetTumKullanıcılar()
        {
            Uri uri = new Uri(UrlHelper.KullanıcıUrl);
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var sonuc = JsonSerializer.Deserialize<List<Kullanıcı>>(content, _serializerOptions);
                return sonuc;
            }
            return new List<Kullanıcı>();
        }

        public async Task KullanıcıEkle(KullanıcıOlusturDto kullanıcı)
        {
            var jsonContent = JsonContent.Create(kullanıcı, options: _serializerOptions);
            HttpResponseMessage response = await _client.PostAsync(UrlHelper.KullanıcıUrl, jsonContent);
            if (response.IsSuccessStatusCode)
            {
                // Handle success
            }
        }

        public async Task KullanıcıGuncelle( kullanıcıGuncelleDto kullanıcı)
        {
            var jsonContent = JsonContent.Create(kullanıcı, options: _serializerOptions);
            HttpResponseMessage response = await _client.PutAsync($"{UrlHelper.KullanıcıUrl}", jsonContent);
            if (response.IsSuccessStatusCode)
            {
                // Başarı durumunu işle
            }
        }

        public async Task KullanıcıSil(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"{UrlHelper.KullanıcıUrl}?id={id}");
            if (response.IsSuccessStatusCode)
            {
                // Handle success
            }
        }
    }

}
