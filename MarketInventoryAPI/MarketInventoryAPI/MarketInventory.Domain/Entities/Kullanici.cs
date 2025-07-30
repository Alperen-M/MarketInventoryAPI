using System;

namespace MarketInventory.Domain.Entities;

public class Kullanici
{
    public int Id { get; set; }

    public string KullaniciAdi { get; set; } = string.Empty;

    public string Sifre { get; set; } = string.Empty;

    public DateTime KayitTarihi { get; set; } = DateTime.UtcNow;

    public DateTime? GuncellemeTarihi { get; set; }

    public DateTime? SilinmeTarihi { get; set; }

    public int KullaniciTuruId { get; set; }
    public KullaniciTuru? KullaniciTuru { get; set; }
}
