namespace MarketInventory.Domain.Entities;
public class Barkod
{
    public int Id { get; set; }
    public string Kod { get; set; } = string.Empty;

   
    public int? UrunId { get; set; }
    public Urun? Urun { get; set; }

    public int BirimId { get; set; }     
    public Birim? Birim { get; set; }

    public bool AktifMi { get; set; } = true;
    public DateTime KayitTarihi { get; set; } = DateTime.UtcNow;
    public DateTime? GuncellemeTarihi { get; set; }
    public DateTime? SilinmeTarihi { get; set; }

    public int? CreatedById { get; set; }
    public Kullanici? CreatedByUser { get; set; }
    public string UrunBirimi { get; set; }
}
