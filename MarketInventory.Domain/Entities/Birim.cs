using System.Collections.Generic;

namespace MarketInventory.Domain.Entities;

public class Birim
{
    public int Id { get; set; }

    public string Ad { get; set; } = string.Empty;

    public decimal Carpan { get; set; }

    public int? CreatedById { get; set; }
    public Kullanici? CreatedByUser { get; set; }

    public ICollection<Urun> Urunler { get; set; } = new List<Urun>();
}

