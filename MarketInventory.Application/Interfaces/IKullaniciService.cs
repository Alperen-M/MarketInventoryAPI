using MarketInventory.Domain.Entities;

namespace MarketInventory.Application.Interfaces
{
    public interface IKullaniciService
    {
        Task<IEnumerable<Kullanici>> GetAllAsync();
        Task<Kullanici?> GetByIdAsync(int id);
        Task<Kullanici> CreateAsync(Kullanici kullanici);
        Task<bool> UpdateAsync(Kullanici kullanici);
        Task<bool> DeleteAsync(int id);
    }
}
