
using ProjeuygulamlarıKulllanıcı.Dtos;
using ProjeuygulamlarıKulllanıcı.Services;

namespace ProjeuygulamlarıKulllanıcı.Sayfalar;

public partial class KullanıcıKayıt : ContentPage
   
{
    private Kullanıcı _kullanıcı;
    private readonly IKullanıcıServices _kullanıcıServices;
	public KullanıcıKayıt()
	{
		InitializeComponent();
        _kullanıcıServices = new KullanıcıServices();
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
		//
    }

    private async void KUllanıcıkAYIT_Clicked(object sender, EventArgs e)
    {
        if( EkullancıAdı!=null && EKullancıSoyadı!=null && EKullanıcıKanGrubu!=null && KullanıcıSifre!=null && KullanıcıTC!= null)
        {
            var eklenecek = new KullanıcıOlusturDto
            {
                kullanıcıAdı = EkullancıAdı.Text.Trim(),
                kullanıcıSoyadı = EKullancıSoyadı.Text.Trim(),
                kullanıcıKanGrubu = EKullanıcıKanGrubu.Text.Trim(),
                kullanıcıSifre = KullanıcıSifre.Text.Trim(),
                kullanıcıTC = KullanıcıTC.Text.Trim(),
                KullanıcıKanVermeTarihi = KullanıcıKanVermeTarih.Date
            };
            await _kullanıcıServices.KullanıcıEkle(eklenecek);
            await DisplayAlert("İşlem başarılı", "", "Tamam");
        
        }

        else
        {
            await DisplayAlert("hatalı kayıt lütfen boş bırakmayınız ", "", "Tamam");
        }

    }
}