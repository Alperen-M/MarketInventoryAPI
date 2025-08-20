namespace MarketInventory.Application.DTOs
{
    public class StokHareketiCreateDto
    {
        public int? UrunId { get; set; }
        public string HareketTuru { get; set; } = string.Empty;
        public int BirimId { get; set; }
        public int Miktar { get; set; }
        public int? CreatedById { get; set; }
    }

    public class StokHareketiUpdateDto
    {
        public int Id { get; set; }
        public int? UrunId { get; set; }
        public string HareketTuru { get; set; } = string.Empty;
        public int BirimId { get; set; }
        public int Miktar { get; set; }
        public int? CreatedById { get; set; }
    }

    public class StokHareketiReadDto
    {
        public int Id { get; set; }
        public int? UrunId { get; set; }
        public string? UrunAdi { get; set; }
        public string HareketTuru { get; set; } = string.Empty;
        public int BirimId { get; set; }
        public string? BirimAdi { get; set; }
        public int Miktar { get; set; }
        public DateTime KayitTarihi { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }
    }
}