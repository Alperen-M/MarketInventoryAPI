namespace MarketInventory.Application.Dtos
{
    public class KullaniciTuruDto
    {
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty;
        
    }

    public class CreateKullaniciTuruDto
    {
        public string Ad { get; set; } = string.Empty;
    }
}
