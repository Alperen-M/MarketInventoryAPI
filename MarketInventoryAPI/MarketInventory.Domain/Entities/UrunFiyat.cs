namespace MarketInventory.Domain.Entities;

public class UrunFiyat
{
    public int Id { get; set; }

    public bool AktifMi { get; set; } = true;

    public DateTime BaslangicTarihi { get; set; }

    public DateTime SonTarih { get; set; }

    public decimal Fiyat { get; set; }

    public string? CreatedBy { get; set; }

    public int? UrunId { get; set; }
    
    public Urun? Urun { get; set; }
}
