using ProjeUygulamlarıHastane.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ProjeUygulamlarıHastane.Services;

namespace ProjeUygulamlarıHastane.Services
{
    public static class UrlHelper
    {
        private static string BaseUrl = "https://localhost:7193";
        public static string HastaUrl = $"{BaseUrl}/Hasta";
    }
    public abstract class BaseService
    {
        protected HttpClient _client;
        protected JsonSerializerOptions _serializerOptions;

        public BaseService()
        {
#if DEBUG && ANDROID
HttpsClientHandlerService handler = new HttpsClientHandlerService();
_client = new HttpClient(handler.GetPlatformMessageHandler());
#else
            _client = new HttpClient();
#endif

                    _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
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

        async Task IHastaServices.HastaEkle(HastaOlusturDto hasta)
        {
            Uri uri = new Uri(UrlHelper.HastaUrl);
            JsonContent content = JsonContent.Create(hasta);

            HttpResponseMessage response = await _client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {

            }
        }

        async Task IHastaServices.HastaGuncelle(HastaGuncelleDto hasta)
        {

            JsonContent jsonContent = JsonContent.Create(hasta);

            await _client.PutAsync(UrlHelper.HastaUrl, jsonContent);
        }
        async public Task HastaSil(int id)
        {
            Uri uri = new Uri($"{UrlHelper.HastaUrl}?id={id}");

            HttpResponseMessage response = await _client.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {

            }
        }


    }
}




