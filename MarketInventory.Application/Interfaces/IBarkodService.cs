using MarketInventory.Domain.Entities;

namespace MarketInventory.Application.Interfaces
{
    public interface IBarkodService
    {
        Task<IEnumerable<Barkod>> GetAllAsync();
        Task<Barkod?> GetByIdAsync(int id);
        Task<Barkod> CreateAsync(Barkod barkod);
        Task<bool> UpdateAsync(Barkod barkod);
        Task<bool> DeleteAsync(int id);
    }
}
