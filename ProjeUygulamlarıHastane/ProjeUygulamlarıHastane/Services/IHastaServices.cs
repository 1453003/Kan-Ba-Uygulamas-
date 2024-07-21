using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjeUygulamlarıHastane.Dtos;

namespace ProjeUygulamlarıHastane.Services
{
    internal  interface IHastaServices
    {
        Task<List<Hasta>> GetTumHastalar();
        Task HastaEkle(HastaOlusturDto hasta);
        Task HastaGuncelle(HastaGuncelleDto hasta);
        Task HastaSil(int id);

    }
}
