using Microsoft.EntityFrameworkCore;
using proje_uygulamaları_api.Models;
using proje_uygulamaları_api.Services;
using proje_uygulamaları_api.Hubs;

namespace proje_uygulamaları_api.Efcore
{
    public class HastaOtomasyonuEklemeDbContext : DbContext
    {
        public HastaOtomasyonuEklemeDbContext(DbContextOptions<HastaOtomasyonuEklemeDbContext> options)
            : base(options)
        {

        }

        public DbSet<Hasta> hastalar { get; set; }
        public DbSet<Kullanıcı> kullanıcılar { get; set; }
    }
}
