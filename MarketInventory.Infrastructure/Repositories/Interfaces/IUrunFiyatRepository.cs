using MarketInventory.Domain.Entities;

namespace MarketInventory.Infrastructure.Repositories.Interfaces
{
    public interface IUrunFiyatRepository : IGenericRepository<UrunFiyat>
    {
        Task<IEnumerable<UrunFiyat>> GetAktifFiyatlarAsync();
    }
}
