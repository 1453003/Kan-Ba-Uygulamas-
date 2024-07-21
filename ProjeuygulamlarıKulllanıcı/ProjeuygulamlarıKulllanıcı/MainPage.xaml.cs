using ProjeuygulamlarıKulllanıcı.Sayfalar;
using ProjeuygulamlarıKulllanıcı.Services;

namespace ProjeuygulamlarıKulllanıcı
{
    public partial class MainPage : ContentPage
    {
        private readonly IKullanıcıServices _kullanıcıServices;
        private readonly IHastaServices _hastaServices;



        public MainPage()
        {
            InitializeComponent();
            _kullanıcıServices = new KullanıcıServices();
            _hastaServices = new HastaServices();


        }
        private async void OnLabelTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new KullanıcıKayıt());
        }

        private async void Giris_Clicked(object sender, EventArgs e)
        {
            var gelenKullanıcı = TC.Text; // Kullanıcı adı giriş alanından alındı
            var gelenSifre = Sifre.Text; // Şifre giriş alanından alındı

            // Kullanıcıları getir
            var kullanıcılar = await _kullanıcıServices.GetTumKullanıcılar();

            // Doğru kullanıcıyı bul
            var kullanıcı = kullanıcılar.FirstOrDefault(x => x.kullanıcıTC == gelenKullanıcı && x.kullanıcıSifre == gelenSifre);

            if (kullanıcı != null)
            {
                // Kullanıcının kan grubunu al
                var kullaniciKanGrubu = kullanıcı.kullanıcıKanGrubu;

                // Hastaları getir
                var hastalar = await _hastaServices.GetTumHastalar();

                // Giriş yapan kullanıcının kan grubuna uyan hastaları bul
                var uyumluHastalar = hastalar.Where(hasta => string.Equals(hasta.KanGrubu, kullaniciKanGrubu, StringComparison.OrdinalIgnoreCase)).ToList();

               
                    // Anasayfa'nın bir örneği oluşturuldu
                    var anasayfa = new Anasayfa(kullanıcı, uyumluHastalar);


                    // Anasayfa'yı göster
                    await Navigation.PushAsync(anasayfa);
               
               
            }
            else
            {
                // Kullanıcı bilgileri yanlışsa uyarı göster
                await DisplayAlert("", "Eksik veya hatalı giriş", "Tamam");
            }
        }

    }


}