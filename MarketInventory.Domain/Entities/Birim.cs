namespace MarketInventory.Domain.Entities;
public class Birim
{
    public int Id { get; set; }
    public string Ad { get; set; } = string.Empty;
    public decimal Carpan { get; set; }

    public int? CreatedById { get; set; }
    public Kullanici? CreatedByUser { get; set; }

    // Navigation
    public ICollection<Urun> Urunler { get; set; } = new List<Urun>();
    public ICollection<Barkod> Barkodlar { get; set; } = new List<Barkod>();
    public ICollection<StokHareketi> StokHareketleri { get; set; } = new List<StokHareketi>();
}