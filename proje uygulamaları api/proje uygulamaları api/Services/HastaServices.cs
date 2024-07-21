using Microsoft.AspNetCore.SignalR;
using proje_uygulamaları_api.Dtos;
using proje_uygulamaları_api.Efcore;
using proje_uygulamaları_api.Hubs;
using proje_uygulamaları_api.Models;
using System.Collections.Generic;
using System.Linq;

namespace proje_uygulamaları_api.Services
{
    public class HastaServices : IHastaServices
    {
        private readonly HastaOtomasyonuEklemeDbContext _dbContext;
        private readonly IHubContext<NotificationHub> _hubContext;

        public HastaServices(HastaOtomasyonuEklemeDbContext dbContext, IHubContext<NotificationHub> hubContext)
        {
            _dbContext = dbContext;
            _hubContext = hubContext;
        }

        public List<Hasta> GetTumHastalar()
        {
            return _dbContext.hastalar.ToList();
        }

        public async void HastaEkle(HastaOlusturDto hasta)
        {
            var eklencekhasta = new Hasta
            {
                Ad = hasta.Ad,
                Soyad = hasta.Soyad,
                TCKimlikNo = hasta.TCKimlikNo,
                DogumTarihi = hasta.DogumTarihi,
                Cinsiyet = hasta.Cinsiyet,
                KanGrubu = hasta.KanGrubu,
                HastaneAdı = hasta.HastaneAdı,
                HastaneAdres = hasta.HastaneAdres,
                HastaneNo = hasta.HastaneNo,
                HastaneSifre = hasta.HastaneSifre,
                KacUnite=hasta.KacUnite,
            };

            _dbContext.hastalar.Add(eklencekhasta);
            _dbContext.SaveChanges();

            // Bildirim gönder
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Yeni bir hasta eklendi");
        }

        public void HastaGuncelle(HastaGuncelleDto hasta)
        {
            var guncellenecek = _dbContext.hastalar.Find(hasta.ıd);
            if (guncellenecek != null)
            {
                guncellenecek.ıd = hasta.ıd;
                guncellenecek.Ad = hasta.Ad;
                guncellenecek.Soyad = hasta.Soyad;
                guncellenecek.TCKimlikNo = hasta.TCKimlikNo;
                guncellenecek.DogumTarihi = hasta.DogumTarihi;
                guncellenecek.Cinsiyet = hasta.Cinsiyet;
                guncellenecek.KanGrubu = hasta.KanGrubu;
                guncellenecek.HastaneAdı = hasta.HastaneAdı;
                guncellenecek.HastaneAdres = hasta.HastaneAdres;
                guncellenecek.HastaneNo = hasta.HastaneNo;
                guncellenecek.HastaneSifre = hasta.HastaneSifre;
                guncellenecek.KacUnite = hasta.KacUnite;

             
            }
            _dbContext.SaveChanges();
        }

        public void HastaSil(int id)
        {
            var hasta = _dbContext.hastalar.Find(id);
            if (hasta != null)
            {
                _dbContext.hastalar.Remove(hasta);
                _dbContext.SaveChanges();
            }
        }
    }
}
