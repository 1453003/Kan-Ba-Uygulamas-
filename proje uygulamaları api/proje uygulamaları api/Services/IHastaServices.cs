using proje_uygulamaları_api.Dtos;
using proje_uygulamaları_api.Models;

namespace proje_uygulamaları_api.Services
{
    public interface IHastaServices
    {
        List<Hasta> GetTumHastalar();
        void HastaEkle(HastaOlusturDto hasta);
        void HastaGuncelle(HastaGuncelleDto hasta);
        void HastaSil(int id);
    }
}
