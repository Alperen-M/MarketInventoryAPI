namespace MarketInventory.Domain.Entities;

public class Barkod
{
    public int Id { get; set; }

    public string Kod { get; set; } = string.Empty;

    public int? UrunId { get; set; }
    
    public Urun? Urun { get; set; }

    public int UrunBirimId { get; set; }
    
    public Birim? UrunBirimi { get; set; }

    public bool AktifMi { get; set; } = true;

    public DateTime KayitTarihi { get; set; } = DateTime.UtcNow;

    public DateTime? GuncellemeTarihi { get; set; }

    public DateTime? SilinmeTarihi { get; set; }

    public string? CreatedBy { get; set; }
}
