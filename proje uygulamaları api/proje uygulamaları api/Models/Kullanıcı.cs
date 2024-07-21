using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proje_uygulamaları_api.Models
{
    [Table("Kullanıcı")]
    public class Kullanıcı
    {
        [Key]
        public  int kullanıcıId { get; set; }
        public string kullanıcıAdı { get; set; }
        public string kullanıcıSoyadı { get; set; }
        public string kullanıcıKanGrubu { get; set; }
        public string kullanıcıSifre { get; set; }
        public string kullanıcıTC { get; set; }
        public DateTime KullanıcıKanVermeTarihi { get; set; }


    }
}
