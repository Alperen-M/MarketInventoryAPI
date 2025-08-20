using MarketInventory.Domain.Entities;

namespace MarketInventory.Infrastructure.Repositories.Interfaces
{
    public interface IBirimRepository : IGenericRepository<Birim>
    {
        Task<IEnumerable<Birim>> GetAllWithUrunAsync();
    }
}