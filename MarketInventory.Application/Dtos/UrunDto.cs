namespace MarketInventory.Application.DTOs
{
    public class UrunCreateDto
    {
        public string Ad { get; set; } = string.Empty;
        public string? Tur { get; set; }
        public int BirimId { get; set; }
        public int? CreatedById { get; set; }
    }

    public class UrunUpdateDto
    {
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string? Tur { get; set; }
        public int BirimId { get; set; }
        public int? CreatedById { get; set; }
    }

    public class UrunReadDto
    {
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string? Tur { get; set; }
        public int BirimId { get; set; }
        public string? BirimAdi { get; set; }
        public DateTime KayitTarihi { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }
    }
}
