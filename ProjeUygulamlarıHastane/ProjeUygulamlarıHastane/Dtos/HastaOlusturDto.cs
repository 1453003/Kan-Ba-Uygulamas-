using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeUygulamlarıHastane.Dtos
{
    internal class HastaOlusturDto
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string TCKimlikNo { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string Cinsiyet { get; set; }
        public string KanGrubu { get; set; }
        public string HastaneAdı { get; set; }
        public string HastaneAdres { get; set; }
        public string HastaneNo { get; set; }
        public string HastaneSifre { get; set; }
        public string KacUnite { get; set; }

    }
}
