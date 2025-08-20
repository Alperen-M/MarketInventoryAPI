namespace MarketInventory.Application.Dtos.Barkod
{
    public class BirimCreateDto
    {
        public string Ad { get; set; } = null!;
        public decimal Carpan { get; set; }
    }

    public class BirimUpdateDto
    {
        public int Id { get; set; }
        public string Ad { get; set; } = null!;
        public decimal Carpan { get; set; }
    }


}