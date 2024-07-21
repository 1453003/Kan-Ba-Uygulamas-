using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeuygulamlarıKulllanıcı.Dtos
{
  public  class KullanıcıOlusturDto
    {
        public string kullanıcıAdı { get; set; }
        public string kullanıcıSoyadı { get; set; }
        public string kullanıcıKanGrubu { get; set; }
        public string kullanıcıSifre { get; set; }
        public string kullanıcıTC { get; set; }
        public DateTime KullanıcıKanVermeTarihi { get; set; }
    }
}
