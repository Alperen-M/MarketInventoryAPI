using MarketInventory.Domain.Entities;

namespace MarketInventory.Application.Interfaces
{
    public interface IBirimService
    {
        Task<IEnumerable<Birim>> GetAllAsync();
        Task<Birim?> GetByIdAsync(int id);
        Task<Birim> CreateAsync(Birim birim);
        Task<bool> UpdateAsync(Birim birim);
        Task<bool> DeleteAsync(int id);
    }
}
