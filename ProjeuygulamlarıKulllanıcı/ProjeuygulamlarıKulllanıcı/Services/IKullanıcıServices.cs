using ProjeuygulamlarıKulllanıcı.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeuygulamlarıKulllanıcı.Services
{
   internal interface IKullanıcıServices
    {
        Task<List<Kullanıcı>> GetTumKullanıcılar();
        Task  KullanıcıEkle(KullanıcıOlusturDto kullanıcı);
        Task KullanıcıGuncelle(kullanıcıGuncelleDto kullanıcı);
        Task KullanıcıSil(int kullanıcıId);

    }
}
