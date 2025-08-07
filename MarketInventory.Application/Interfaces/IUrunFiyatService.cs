using MarketInventory.Application.Interfaces;
using MarketInventory.Domain.Entities;

namespace MarketInventory.Application.Services.Interfaces
{
    public interface IUrunFiyatService : IGenericService<UrunFiyat>
    {
        Task<IEnumerable<UrunFiyat>> GetFiyatlarByUrunIdAsync(int urunId);
        Task<IEnumerable<UrunFiyat>> GetAktifFiyatlarAsync();
        Task<UrunFiyat?> GetEnGuncelFiyatAsync(int urunId);
        Task<IEnumerable<UrunFiyat>> GetFiyatlarByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<UrunFiyat>> GetFiyatlarByDateRangeAsync(int urunId, DateTime startDate, DateTime endDate);
    }


}
