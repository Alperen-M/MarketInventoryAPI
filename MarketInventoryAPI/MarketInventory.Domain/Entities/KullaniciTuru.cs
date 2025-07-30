namespace MarketInventory.Domain.Entities;

public class KullaniciTuru
{
    public int Id { get; set; }

    public string Ad { get; set; } = string.Empty; // Örn: Admin, Depocu, Yönetici

    public ICollection<Kullanici> Kullanicilar { get; set; } = new List<Kullanici>();
}
