using MarketInventory.Application.Interfaces;
using MarketInventory.Domain.Entities;

using MarketInventory.Application.DTOs.UrunFiyat;

namespace MarketInventory.Application.Services.Interfaces
{
    public interface IUrunFiyatService
    {
        Task<IEnumerable<UrunFiyatDto>> GetAllAsync();
        Task<UrunFiyatDto?> GetByIdAsync(int id);
        Task CreateAsync(CreateUrunFiyatDto dto);
        Task UpdateAsync(UpdateUrunFiyatDto dto);
        Task DeleteAsync(int id);

        Task<IEnumerable<UrunFiyatDto>> GetFiyatlarByUrunIdAsync(int urunId);
        Task<IEnumerable<UrunFiyatDto>> GetAktifFiyatlarAsync();
        Task<UrunFiyatDto?> GetEnGuncelFiyatAsync(int urunId);
        Task<IEnumerable<UrunFiyatDto>> GetFiyatlarByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<UrunFiyatDto>> GetFiyatlarByDateRangeAsync(int urunId, DateTime startDate, DateTime endDate);
    }
}