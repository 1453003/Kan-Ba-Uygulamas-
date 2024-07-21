using proje_uygulamaları_api.Dtos;

using proje_uygulamaları_api.Models;

namespace proje_uygulamaları_api.Services
{
    public interface IkullanıcıServices
    {
        List<Kullanıcı> GetTumKullanıcılar();
        void KullanıcıEkle(KullanıcıOlusturDto kullanıcı);

        void KullanıcıGüncelle(kullanıcıGuncelleDto kullanıcı);
        void KullanıcıSil(int kullanıcıId);

    }
}
