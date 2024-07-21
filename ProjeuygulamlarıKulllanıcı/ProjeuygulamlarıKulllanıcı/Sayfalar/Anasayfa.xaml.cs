using Microsoft.AspNetCore.SignalR.Client;
using ProjeuygulamlarıKulllanıcı.Services;
using ProjeuygulamlarıKulllanıcı.Dtos;
using Microsoft.Extensions.Logging;

using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ProjeuygulamlarıKulllanıcı.Sayfalar
{
    public partial class Anasayfa : ContentPage
    {
        private readonly IHastaServices _hastaServices;
        private readonly Kullanıcı _kullanıcı;
        private readonly HubConnection _hubConnection;
        private bool _isEligibleForDonation;

        public Anasayfa(Kullanıcı kullanıcı, List<Hasta> uyumluHastalar)
        {
            InitializeComponent();

            _kullanıcı = kullanıcı;
            _hastaServices = new HastaServices();

            SetKullanıcı(kullanıcı);
            SetUyumluHastalar(uyumluHastalar);

            // SignalR hub bağlantısını oluştur
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7193/notificationHub")
                .ConfigureLogging(logging =>
                {
                    logging.SetMinimumLevel(LogLevel.Debug);
                    logging.AddConsole();
                })
                .Build();

            // SignalR hub'ından gelen bildirimleri dinle
            _hubConnection.On<string>("YeniHastaEklendi", message =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert("Bildirim", message, "Tamam");
                    await GetUyumluHastalar();
                });
            });

            // SignalR bağlantısını başlat
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

        private void SetKullanıcı(Kullanıcı kullanıcı)
        {
            if (kullanıcı != null)
            {
                kullanıcıAdı.Text = kullanıcı.kullanıcıAdı;
                kullanıcıSoyadı.Text = kullanıcı.kullanıcıSoyadı;
                sonKanVermeTarihiLabel.Text = kullanıcı.KullanıcıKanVermeTarihi.ToString("dd MMM yyyy");

                var remainingDays = CalculateRemainingDays(kullanıcı.KullanıcıKanVermeTarihi);
                _isEligibleForDonation = remainingDays == 0;

                remainingDaysLabel.Text = _isEligibleForDonation
                         ? "Kan verebilirsin."
                         : $" kan vermene {remainingDays} gün kaldı.";
                remainingDaysLabel.BackgroundColor = _isEligibleForDonation ? Colors.Green : Colors.Red;


            }
        }

        private int CalculateRemainingDays(DateTime lastDonationDate)
        {
            var daysSinceLastDonation = (DateTime.Now - lastDonationDate).TotalDays;
            var daysUntilEligible = 90 - daysSinceLastDonation;
            return daysUntilEligible > 0 ? (int)daysUntilEligible : 0;
        }

        private void SetUyumluHastalar(List<Hasta> uyumluHastalar)
        {
            if (uyumluHastalar != null && uyumluHastalar.Any())
            {
                collectionViewHasta.ItemsSource = uyumluHastalar;
            }
            else
            {
                collectionViewHasta.ItemsSource = null;
            }
        }

        private async void ContentPage_Loaded(object sender, EventArgs e)
        {
            await GetUyumluHastalar();
        }

        private async Task GetUyumluHastalar()
        {
            var kullaniciKanGrubu = _kullanıcı.kullanıcıKanGrubu;
            var hastalar = await _hastaServices.GetTumHastalar();
            var uyumluHastalar = hastalar.Where(hasta => string.Equals(hasta.KanGrubu, kullaniciKanGrubu, StringComparison.OrdinalIgnoreCase)).ToList();
            SetUyumluHastalar(uyumluHastalar);
        }

        private async void Ayarlar(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Ayarlar());
        }

        private async void OnGuncelleClicked(object sender, EventArgs e)
        {
            var yeniTarih = kanVermePicker.Date;
            _kullanıcı.KullanıcıKanVermeTarihi = yeniTarih;

            var kullanıcıGuncelleDto = new kullanıcıGuncelleDto
            {
                kullanıcıId = _kullanıcı.kullanıcıId,
                kullanıcıAdı = _kullanıcı.kullanıcıAdı,
                kullanıcıSoyadı = _kullanıcı.kullanıcıSoyadı,
                kullanıcıKanGrubu = _kullanıcı.kullanıcıKanGrubu,
                kullanıcıSifre = _kullanıcı.kullanıcıSifre,
                kullanıcıTC = _kullanıcı.kullanıcıTC,
                KullanıcıKanVermeTarihi = yeniTarih
            };

            try
            {
                // Kullanıcı güncelleme isteğini yap
                var kullanıcıService = new KullanıcıServices(); // Burada DI kullanarak servisi elde etmek daha iyi olurdu
                await kullanıcıService.KullanıcıGuncelle(kullanıcıGuncelleDto);

                // Kullanıcı güncelleme başarılı olduğunda kullanıcıya bildirim göster
                await DisplayAlert("Başarılı", "Kullanıcı güncelleme başarılı.", "Tamam");

                // Kullanıcı bilgisini güncelle
                _kullanıcı.KullanıcıKanVermeTarihi = yeniTarih;
                SetKullanıcı(_kullanıcı);
            }
            catch (Exception ex)
            {
                // Hata durumunda kullanıcıya bildirim göster
                await DisplayAlert("Hata", "Kullanıcı güncelleme işlemi sırasında hata oluştu: " + ex.Message, "Tamam");
            }
        }
    }
}
