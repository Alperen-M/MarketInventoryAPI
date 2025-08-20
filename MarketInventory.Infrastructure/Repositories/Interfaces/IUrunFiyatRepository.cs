using MarketInventory.Domain.Entities;

namespace MarketInventory.Infrastructure.Repositories.Interfaces
{
    public interface IUrunFiyatRepository : IGenericRepository<UrunFiyat>
    {
        Task DeleteAsync(UrunFiyat entity);
        Task<IEnumerable<UrunFiyat>> GetAktifFiyatlarAsync();
        Task UpdateAsync(UrunFiyat entity);
    }
}