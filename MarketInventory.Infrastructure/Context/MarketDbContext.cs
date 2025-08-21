using MarketInventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MarketInventory.Infrastructure.Data
{
    public class MarketDbContext : DbContext
    {
        public MarketDbContext(DbContextOptions<MarketDbContext> options)
            : base(options) { }

        public virtual DbSet<Urun> Urunler { get; set; } = null!;
        public DbSet<Birim> Birimler { get; set; } = null!;
        public DbSet<Barkod> Barkodlar { get; set; } = null!;
        public DbSet<UrunFiyat> UrunFiyatlar { get; set; } = null!;
        public DbSet<StokHareketi> StokHareketleri { get; set; } = null!;
        public DbSet<Kullanici> Kullanicilar { get; set; } = null!;
        public DbSet<KullaniciTuru> KullaniciTurleri { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Barkod.Kod unique & length
            modelBuilder.Entity<Barkod>()
                .HasIndex(b => b.Kod)
                .IsUnique();

            // decimal precision
            modelBuilder.Entity<Birim>()
                .Property(b => b.Carpan)
                .HasPrecision(18, 2);

            modelBuilder.Entity<StokHareketi>()
                .Property(s => s.Miktar)
                .HasPrecision(18, 2);

            modelBuilder.Entity<UrunFiyat>()
                .Property(f => f.Fiyat)
                .HasPrecision(18, 2);

            // Urun <-> Birim (required)
            modelBuilder.Entity<Urun>()
                .HasOne(u => u.Birim)
                .WithMany(b => b.Urunler)
                .HasForeignKey(u => u.BirimId)
                .OnDelete(DeleteBehavior.Restrict);

            // Barkod -> Urun (optional)
            modelBuilder.Entity<Barkod>()
                .HasOne(b => b.Urun)
                .WithMany(u => u.Barkodlar)
                .HasForeignKey(b => b.UrunId)
                .OnDelete(DeleteBehavior.SetNull);

            // Barkod -> Birim
            modelBuilder.Entity<Barkod>()
                .HasOne(b => b.Birim)
                .WithMany(birim => birim.Barkodlar)
                .HasForeignKey(b => b.BirimId)
                .OnDelete(DeleteBehavior.Restrict);

            // StokHareketi -> Urun (optional)
            modelBuilder.Entity<StokHareketi>()
                .HasOne(s => s.Urun)
                .WithMany(u => u.StokHareketleri)
                .HasForeignKey(s => s.UrunId)
                .OnDelete(DeleteBehavior.SetNull);

            // StokHareketi -> Birim
            modelBuilder.Entity<StokHareketi>()
                .HasOne(s => s.Birim)
                .WithMany(b => b.StokHareketleri)
                .HasForeignKey(s => s.BirimId)
                .OnDelete(DeleteBehavior.Restrict);

            // UrunFiyat -> Urun
            modelBuilder.Entity<UrunFiyat>()
                .HasOne(f => f.Urun)
                .WithMany(u => u.Fiyatlar)
                .HasForeignKey(f => f.UrunId)
                .OnDelete(DeleteBehavior.Cascade);

            // Kullanici <-> KullaniciTuru
            modelBuilder.Entity<Kullanici>()
                .HasOne(k => k.KullaniciTuru)
                .WithMany(t => t.Kullanicilar)
                .HasForeignKey(k => k.KullaniciTuruId)
                .OnDelete(DeleteBehavior.Restrict);

            // Self-reference: Kullanici.CreatedByUser (nullable)
            modelBuilder.Entity<Kullanici>()
                .HasOne(k => k.CreatedByUser)
                .WithMany(kp => kp.CreatedUsers)
                .HasForeignKey(k => k.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // CreatedBy relationships for other entities (optional)
            modelBuilder.Entity<Urun>()
                .HasOne(u => u.CreatedByUser)
                .WithMany()
                .HasForeignKey(u => u.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Birim>()
                .HasOne(b => b.CreatedByUser)
                .WithMany()
                .HasForeignKey(b => b.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Barkod>()
                .HasOne(b => b.CreatedByUser)
                .WithMany()
                .HasForeignKey(b => b.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UrunFiyat>()
                .HasOne(f => f.CreatedByUser)
                .WithMany()
                .HasForeignKey(f => f.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StokHareketi>()
                .HasOne(s => s.CreatedByUser)
                .WithMany()
                .HasForeignKey(s => s.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
