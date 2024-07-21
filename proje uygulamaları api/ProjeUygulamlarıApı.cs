
using ProjeUygulamlarıApi.Models;
using ProjeUygulamlarıApi.Dtos;

namespace ProjeUygulamlarıApi.Services
{
    public interface IHastaServices
    {
        List<Hasta> GetTumHastalar();
        
        void HastaEkle(HastaOlusturDto hasta);
        void HastaGuncelle(HastaGuncelleDto hasta);
        void HastaSil(int id);

    }
}
