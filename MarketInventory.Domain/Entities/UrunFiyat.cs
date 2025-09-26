namespace MarketInventory.Domain.Entities;
public class UrunFiyat
{
    public int Id { get; set; }
    public bool AktifMi { get; set; } = true;
    public DateTime BaslangicTarihi { get; set; } = DateTime.UtcNow;
    public DateTime? SonTarih { get; set; }   
    public decimal Fiyat { get; set; }

    public int? CreatedById { get; set; }
    public Kullanici? CreatedByUser { get; set; }

    public int UrunId { get; set; }
    public Urun? Urun { get; set; }
}
