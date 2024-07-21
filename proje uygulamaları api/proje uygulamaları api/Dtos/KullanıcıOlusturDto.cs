using proje_uygulamaları_api.Models;

namespace proje_uygulamaları_api.Dtos
{
    public class KullanıcıOlusturDto
    {
        
        public string kullanıcıAdı { get; set; }
        public string kullanıcıSoyadı { get; set; }
        public string kullanıcıKanGrubu { get; set; }
        public string kullanıcıSifre { get; set; }
        public string kullanıcıTC { get; set; }
        public DateTime KullanıcıKanVermeTarihi { get; set; }
    }
}
