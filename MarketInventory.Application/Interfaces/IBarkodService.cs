using MarketInventory.Domain.Entities;

namespace MarketInventory.Application.Interfaces
{
    public interface IBarkodService
    {
        Task<IEnumerable<Barkod>> GetAllAsync();
        Task<Barkod?> GetByIdAsync(int id);
        Task<Barkod> AddAsync(Barkod entity);
        Task<Barkod> UpdateAsync(Barkod entity);
        Task DeleteAsync(int id);
    }
}
