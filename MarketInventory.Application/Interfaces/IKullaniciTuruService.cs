using MarketInventory.Domain.Entities;

namespace MarketInventory.Application.Interfaces
{
    public interface IKullaniciTuruService
    {
        Task<IEnumerable<KullaniciTuru>> GetAllAsync();
        Task<KullaniciTuru?> GetByIdAsync(int id);
        Task<KullaniciTuru> CreateAsync(KullaniciTuru tur);
        Task<bool> UpdateAsync(KullaniciTuru tur);
        Task<bool> DeleteAsync(int id);
    }
}
