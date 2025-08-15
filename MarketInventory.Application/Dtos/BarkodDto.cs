namespace MarketInventory.Application.Dtos.Barkod
{
    public class BarkodCreateDto
    {
        public string Kod { get; set; } = string.Empty;
        public int? UrunId { get; set; }
        public int UrunBirimId { get; set; }
        public bool AktifMi { get; set; } = true;
        public int CreatedById { get; set; }
        public string UrunBirimi { get; set; }
        
    }

    public class BarkodUpdateDto
    {
        public int Id { get; set; }
        public string Kod { get; set; } = string.Empty;
        public int? UrunId { get; set; }
        public int BirimId { get; set; }
        public bool AktifMi { get; set; }
    }

    public class BarkodDetailDto
    {
        public int Id { get; set; }
        public string Kod { get; set; } = string.Empty;
        public string? UrunAdi { get; set; }
        public string? BirimAdi { get; set; }
        public bool AktifMi { get; set; }
        public DateTime KayitTarihi { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }
    }
}
