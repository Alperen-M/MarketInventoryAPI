using MarketInventory.Domain.Entities;

namespace MarketInventory.Application.Interfaces
{
    public interface IUrunFiyatService
    {
        Task<IEnumerable<UrunFiyat>> GetAllAsync();
        Task<UrunFiyat?> GetByIdAsync(int id);
        Task<UrunFiyat> CreateAsync(UrunFiyat fiyat);
        Task<bool> UpdateAsync(UrunFiyat fiyat);
        Task<bool> DeleteAsync(int id);
    }
}
