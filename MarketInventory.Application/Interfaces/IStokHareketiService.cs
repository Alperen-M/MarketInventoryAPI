using MarketInventory.Domain.Entities;

namespace MarketInventory.Application.Interfaces
{
    public interface IStokHareketiService
    {
        Task<IEnumerable<StokHareketi>> GetAllAsync();
        Task<StokHareketi?> GetByIdAsync(int id);
        Task<StokHareketi> CreateAsync(StokHareketi hareket);
        Task<bool> UpdateAsync(StokHareketi hareket);
        Task<bool> DeleteAsync(int id);
    }
}
