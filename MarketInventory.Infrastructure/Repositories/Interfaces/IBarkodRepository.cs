using MarketInventory.Domain.Entities;

namespace MarketInventory.Infrastructure.Repositories.Interfaces
{
    public interface IBarkodRepository : IGenericRepository<Barkod>
    {
        // Belirli bir koda sahip barkodun var olup olmadığını asenkron olarak kontrol eder.
        // Bu metot, AddAsync metodundaki iş mantığı için gereklidir.
        Task<bool> BarkodExistsAsync(string barkodKod);

        // Bir barkodu asenkron olarak siler.
        Task DeleteAsync(Barkod barkod);

        // Bir barkodu asenkron olarak günceller.
        Task UpdateAsync(Barkod barkod);

        // Aktif olan tüm barkodları asenkron olarak alır.
        Task<IEnumerable<Barkod>> GetAktifBarkodlarAsync();
    }
}