using ProjeUygulamlarıHastane.Dtos;
using ProjeUygulamlarıHastane.Services;


namespace ProjeUygulamlarıHastane.Sayaflar;

public partial class Anasayfa : ContentPage
{
    private Hasta _hasta;
    private readonly IHastaServices _hastaServices;
    public void SetHasta(Hasta hasta)
    {
        _hasta = hasta;
    }
    public Anasayfa()
    {
        InitializeComponent();
        _hastaServices = new HastaServices();


    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        await GetHastalar();
    }

    private async Task GetHastalar()
    {
        var hastalar = await _hastaServices.GetTumHastalar();
        collectionViewHasta.ItemsSource = hastalar;
    }

    private async void HastaEKle_Clicked(object sender, EventArgs e)
    {
        if (EHastaAdı != null && EHastaSoyadı != null && EKanGrubu != null)
        {


            var dogumTarihi = DateTime.Now;
            var eklenecek = new HastaOlusturDto
            {
                Ad = EHastaAdı.Text.Trim(),
                Soyad = EHastaSoyadı.Text.Trim(),
                TCKimlikNo = EHastaTc.Text.Trim(),
                KanGrubu = EKanGrubu.Text.Trim(),
                HastaneSifre = "12345",
                Cinsiyet = "erkek",
                DogumTarihi = dogumTarihi,
                HastaneNo = "12345",
                HastaneAdı = " Isparta Şehir Hastanesi",
                HastaneAdres = "Isparta Şehir Hastanesi Otoparkları, 32200 Isparta/Sanayi",
                KacUnite = KaçUnite.Text.Trim()




            };
            await _hastaServices.HastaEkle(eklenecek);

            await DisplayAlert("İşlem başarılı", "Hasta kaydedildi", "Tamam");

            await GetHastalar();



        }
        else
        {
            await DisplayAlert("hatalı kayıt lütfen boş bırakmayınız ", "", "Tamam");
        }

    }

    private async void HastaGetir_Clicked(object sender, EventArgs e)
    {
        if (EHastaTc.Text != null)
        {
            var hastalar = await _hastaServices.GetTumHastalar();
            var hasta = hastalar.FirstOrDefault(x => x.TCKimlikNo == EHastaTc.Text.Trim());
            EHastaAdı.Text = hasta.Ad.ToString();
            EHastaSoyadı.Text = hasta.Soyad.ToString();
            EKanGrubu.Text = hasta.KanGrubu.ToString();
            EHastaTc.Text = hasta.TCKimlikNo.ToString();
            KaçUnite.Text = hasta.KacUnite.ToString();

        }
        else
        {
            await DisplayAlert("lütfen önce tc giriniz", " ", "Tamam");
        }

    }

    private async void HastaGuncelle_Clicked(object sender, EventArgs e)
    {
        if (EHastaTc.Text != null)
        {
            var hastalar = await _hastaServices.GetTumHastalar();
            var hasta = hastalar.FirstOrDefault(x => x.TCKimlikNo == EHastaTc.Text.Trim());

            var guncellenecek = new HastaGuncelleDto
            {
                Ad = EHastaAdı.Text.Trim(),
                Soyad = EHastaSoyadı.Text.Trim(),
                HastaneSifre = hasta.HastaneSifre,
                DogumTarihi = hasta.DogumTarihi,
                Cinsiyet = hasta.Cinsiyet,
                HastaneNo = hasta.HastaneNo,
                KanGrubu = EKanGrubu.Text.Trim(),
                TCKimlikNo = EHastaTc.Text.Trim(),
                ıd = hasta.ıd,
                HastaneAdı = hasta.HastaneAdı,
                HastaneAdres = hasta.HastaneAdres,
                KacUnite = KaçUnite.Text.Trim()



            };
             
            collectionViewHasta.ItemsSource = hastalar;
            await _hastaServices.HastaGuncelle(guncellenecek);

            await DisplayAlert("", "İştem Tamamlandı", "Tamam");
            await GetHastalar();
        }


    }

    private async void HastaSil_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(EHastaTc.Text))
        {
            var hastalar = await _hastaServices.GetTumHastalar();
            var hasta = hastalar.FirstOrDefault(h => h.TCKimlikNo == EHastaTc.Text.Trim());

            if (hasta != null)
            {
                await _hastaServices.HastaSil(hasta.ıd);
                await DisplayAlert("Bilgi", "Hasta başarıyla silindi.", "Tamam");
                await GetHastalar(); // Silme işleminden sonra listeyi güncelle
            }
            else
            {
                await DisplayAlert("Bilgi", "Bu TC'ye sahip bir hasta bulunamadı.", "Tamam");
            }
        }
        else
        {
            await DisplayAlert("Hata", "Lütfen TC kimlik numarasını giriniz.", "Tamam");
        }
    }

}
