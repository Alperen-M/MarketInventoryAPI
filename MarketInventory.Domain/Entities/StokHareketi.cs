namespace MarketInventory.Domain.Entities;
public class StokHareketi
{
    public int Id { get; set; }
    public int? UrunId { get; set; }
    public Urun? Urun { get; set; }

    public DateTime KayitTarihi { get; set; } = DateTime.UtcNow;
    public DateTime? GuncellemeTarihi { get; set; }
    public DateTime? SilinmeTarihi { get; set; }

    public string HareketTuru { get; set; } = string.Empty; // "Giriş"/"Çıkış"
    public int BirimId { get; set; }
    public Birim? Birim { get; set; }

    // stok miktarı decimal olmalı
    public decimal Miktar { get; set; }

    public int? CreatedById { get; set; }
    public Kullanici? CreatedByUser { get; set; }
}