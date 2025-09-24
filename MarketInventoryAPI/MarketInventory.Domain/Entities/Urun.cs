namespace MarketInventory.Domain.Entities;

public class Urun
{
    public int Id { get; set; }

    public string Ad { get; set; } = string.Empty;

    public string? Tur { get; set; }

    public DateTime KayitTarihi { get; set; } = DateTime.UtcNow;

    public DateTime? GuncellemeTarihi { get; set; }

    public DateTime? SilinmeTarihi { get; set; }

    public int BirimId { get; set; }
    
    public Birim? Birim { get; set; }

    public string? CreatedBy { get; set; }

    public ICollection<Barkod> Barkodlar { get; set; } = new List<Barkod>();
    public ICollection<UrunFiyat> Fiyatlar { get; set; } = new List<UrunFiyat>();
    public ICollection<StokHareketi> StokHareketleri { get; set; } = new List<StokHareketi>();
}
