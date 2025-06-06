﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeuygulamlarıKulllanıcı.Dtos
{
    public class Kullanıcı
    {
        [Key]
        public int kullanıcıId { get; set; }
        public string kullanıcıAdı { get; set; }
        public string kullanıcıSoyadı { get; set; }
        public string kullanıcıKanGrubu { get; set; }
        public string kullanıcıSifre { get; set; }
        public string kullanıcıTC { get; set; }
        public DateTime KullanıcıKanVermeTarihi { get; set; }
    }
}
