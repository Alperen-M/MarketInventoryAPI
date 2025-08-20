using MarketInventory.Application.Dtos;


namespace MarketInventory.Application.Dtos
{
    public class LoginDto
    {
        public string KullaniciAdi { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}