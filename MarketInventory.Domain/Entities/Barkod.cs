namespace MarketInventory.Domain.Entities;
public class Barkod
{
    public int Id { get; set; }
    public string Kod { get; set; } = string.Empty;

    // Barkod'un ürüne bağlı olması isteniyorsa non-null; istersen nullable bırak:
    public int? UrunId { get; set; }
    public Urun? Urun { get; set; }

    public int BirimId { get; set; }     // hangi birim ile okutulacak (koli/adet vs)
    public Birim? Birim { get; set; }

    public bool AktifMi { get; set; } = true;
    public DateTime KayitTarihi { get; set; } = DateTime.UtcNow;
    public DateTime? GuncellemeTarihi { get; set; }
    public DateTime? SilinmeTarihi { get; set; }

    public int? CreatedById { get; set; }
    public Kullanici? CreatedByUser { get; set; }
    public string UrunBirimi { get; set; }
}