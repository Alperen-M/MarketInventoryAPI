namespace MarketInventory.Domain.Entities;

public class StokHareketi
{
    public int? UrunId { get; set; }
    public Urun? Urun { get; set; }

    public int Id { get; set; }

    public DateTime KayitTarihi { get; set; } = DateTime.UtcNow;

    public DateTime? GuncellemeTarihi { get; set; }

    public DateTime? SilinmeTarihi { get; set; }

    public string HareketTuru { get; set; } = string.Empty;

    public int BirimId { get; set; }
    public Birim? Birim { get; set; }

    public decimal Miktar { get; set; }

    public string? CreatedBy { get; set; }
}
