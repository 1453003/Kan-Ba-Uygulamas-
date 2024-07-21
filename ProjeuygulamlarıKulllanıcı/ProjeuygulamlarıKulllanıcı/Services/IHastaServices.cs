using ProjeuygulamlarıKulllanıcı.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace ProjeuygulamlarıKulllanıcı.Services
{
    internal interface IHastaServices
    {
        Task<List<Hasta>> GetTumHastalar();
        Task HastaEkle(HastaOlusturDto hasta);
        Task HastaGuncelle(HastaGuncelleDto hasta);
        Task HastaSil(int id);
    }
}

