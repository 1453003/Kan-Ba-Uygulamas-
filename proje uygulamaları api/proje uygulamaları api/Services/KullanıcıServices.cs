using proje_uygulamaları_api.Dtos;
using proje_uygulamaları_api.Efcore;
using proje_uygulamaları_api.Models;
using System.Collections.Generic;
using System.Linq;

namespace proje_uygulamaları_api.Services
{
    public class KullanıcıServices : IkullanıcıServices
    {
        private readonly HastaOtomasyonuEklemeDbContext _dbContext;

        public KullanıcıServices(HastaOtomasyonuEklemeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Kullanıcı> GetTumKullanıcılar()
        {
            return _dbContext.kullanıcılar.ToList();
        }

        public void KullanıcıEkle(KullanıcıOlusturDto kullanıcı)
        {
            var eklencekkullanıcı = new Kullanıcı
            {
                kullanıcıAdı = kullanıcı.kullanıcıAdı,
                kullanıcıSoyadı = kullanıcı.kullanıcıSoyadı,
                kullanıcıTC = kullanıcı.kullanıcıTC,
                kullanıcıKanGrubu = kullanıcı.kullanıcıKanGrubu,
                kullanıcıSifre = kullanıcı.kullanıcıSifre,
                KullanıcıKanVermeTarihi=kullanıcı.KullanıcıKanVermeTarihi
            };

            _dbContext.kullanıcılar.Add(eklencekkullanıcı);
            _dbContext.SaveChanges();
        }
        public void KullanıcıGüncelle(kullanıcıGuncelleDto kullanıcı)
        {
            var guncellenicek = _dbContext.kullanıcılar.Find(kullanıcı.kullanıcıId);
            if (guncellenicek!=null)
            {
                guncellenicek.kullanıcıId = kullanıcı.kullanıcıId;
                guncellenicek.kullanıcıAdı = kullanıcı.kullanıcıAdı;
                guncellenicek.kullanıcıSoyadı = kullanıcı.kullanıcıSoyadı;
                guncellenicek.kullanıcıKanGrubu = kullanıcı.kullanıcıKanGrubu;
                guncellenicek.kullanıcıSifre = kullanıcı.kullanıcıSifre;
                guncellenicek.KullanıcıKanVermeTarihi = kullanıcı.KullanıcıKanVermeTarihi;

              
            };
            _dbContext.SaveChanges();
        }

        public void KullanıcıSil(int kullanıcıId)
        {
            var kullanıcı = _dbContext.kullanıcılar.Find(kullanıcıId);
            if (kullanıcı != null)
            {
                _dbContext.kullanıcılar.Remove(kullanıcı);
                _dbContext.SaveChanges();
            }
        }
    }
}
