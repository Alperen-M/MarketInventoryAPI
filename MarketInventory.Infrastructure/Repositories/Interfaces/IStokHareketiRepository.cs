using MarketInventory.Domain.Entities;

namespace MarketInventory.Infrastructure.Repositories.Interfaces
{
    public interface IStokHareketiRepository : IGenericRepository<StokHareketi>
    {
        Task<IEnumerable<StokHareketi>> GetWithUrunAsync();
    }
}