namespace MarketInventory.Application.DTOs
{
    public class KullaniciCreateDto
    {
        public string KullaniciAdi { get; set; } = string.Empty;
        public string Sifre { get; set; } = string.Empty;
        public int KullaniciTuruId { get; set; }
        public int CreatedById { get; set; }
        
    }

    public class KullaniciUpdateDto
    {
        public int Id { get; set; }
        public string KullaniciAdi { get; set; } = string.Empty;
        public int KullaniciTuruId { get; set; }
        public int CreatedById { get; set; }
    }

    public class KullaniciReadDto
    {
        public int Id { get; set; }
        public string KullaniciAdi { get; set; } = string.Empty;
        public int KullaniciTuruId { get; set; }
        public DateTime KayitTarihi { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }
    }
}
