using ProjeUygulamlarıHastane.Sayaflar;
using ProjeUygulamlarıHastane.Services;

namespace ProjeUygulamlarıHastane
{
    public partial class MainPage : ContentPage
    {
        private readonly IHastaServices _hastaServices;

        public MainPage()
        {
            InitializeComponent();
            _hastaServices = new HastaServices();
           
        }

        private async void GirisButton_Clicked(object sender, EventArgs e)
        {
            var gelenHastaneNo = TC.Text;
            var gelenSifre = Sifre.Text;
            var hastalar = await _hastaServices.GetTumHastalar();
            var hasta = hastalar.FirstOrDefault(x => x.HastaneNo == gelenHastaneNo);
            
            if (hasta!=null && hasta.HastaneNo==gelenHastaneNo && hasta.HastaneSifre==gelenSifre)
            {
                Anasayfa anasayfa = new Anasayfa();
               anasayfa.SetHasta(hasta);
                await Navigation.PushAsync(anasayfa);
            }
            else
            {
                DisplayAlert("", "eksik veya hatalı giriş", "tamam");
            }



            

        }

    }

}
