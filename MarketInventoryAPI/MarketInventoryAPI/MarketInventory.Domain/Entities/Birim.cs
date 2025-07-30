using System.Collections.Generic;

namespace MarketInventory.Domain.Entities;

public class Birim
{
    public int Id { get; set; }

    public string Ad { get; set; } = string.Empty; // Örn: Adet, Koli, Kg, Paket

    public decimal Carpan { get; set; } // Örn: Koli için 30, Adet için 1

    public string? CreatedBy { get; set; }

    public ICollection<Urun> Urunler { get; set; } = new List<Urun>();
}
