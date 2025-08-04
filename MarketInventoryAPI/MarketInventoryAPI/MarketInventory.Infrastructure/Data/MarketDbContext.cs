using MarketInventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MarketInventory.Infrastructure.Data
{
    public class MarketDbContext : DbContext
    {
        public MarketDbContext(DbContextOptions<MarketDbContext> options)
            : base(options)
        {
        
        }

        public DbSet<Urun> Urunler { get; set; } = null!;
        public DbSet<Birim> Birimler { get; set; } = null!;
        public DbSet<Barkod> Barkodlar { get; set; } = null!;
        public DbSet<UrunFiyat> UrunFiyatlar { get; set; } = null!;
        public DbSet<StokHareketi> StokHareketleri { get; set; } = null!;
        public DbSet<Kullanici> Kullanicilar { get; set; } = null!;
        public DbSet<KullaniciTuru> KullaniciTurleri { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Burada Fluent API konfigürasyonlarını yazacağız

            // Örnek: Barkod.Kod alanı benzersiz olmalı
            modelBuilder.Entity<Barkod>()
                .HasIndex(b => b.Kod)
                .IsUnique();

            // Soft delete için global query filter örneği (örneğin Urun için)
            modelBuilder.Entity<Urun>().HasQueryFilter(u => u.SilinmeTarihi == null);

            // İlişkiler otomatik algılanır ama örnek vermek gerekirse:
            modelBuilder.Entity<Urun>()
                .HasOne(u => u.Birim)
                .WithMany(b => b.Urunler)
                .HasForeignKey(u => u.BirimId)
                .OnDelete(DeleteBehavior.Restrict);

            // Kullanici ile KullaniciTuru arasındaki ilişki
            modelBuilder.Entity<Kullanici>()
                .HasOne(k => k.KullaniciTuru)
                .WithMany(t => t.Kullanicilar)
                .HasForeignKey(k => k.KullaniciTuruId)
                .OnDelete(DeleteBehavior.Restrict);

            // Diğer ilişkileri senin ihtiyacına göre buraya ekleyebiliriz
        }
    }
}
