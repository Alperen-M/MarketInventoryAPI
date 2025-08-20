using MarketInventory.Domain.Entities;

namespace MarketInventory.Infrastructure.Repositories.Interfaces
{
    public interface IBarkodRepository : IGenericRepository<Barkod>
    {
        Task DeleteAsync(Barkod barkod);
        Task<IEnumerable<Barkod>> GetAktifBarkodlarAsync();
        Task UpdateAsync(Barkod barkod);
    }
}