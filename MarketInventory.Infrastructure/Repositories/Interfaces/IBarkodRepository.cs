using MarketInventory.Domain.Entities;

namespace MarketInventory.Infrastructure.Repositories.Interfaces
{
    public interface IBarkodRepository : IGenericRepository<Barkod>
    {
       
        Task<IEnumerable<Barkod>> GetAktifBarkodlarAsync();
    }
}
