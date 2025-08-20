using MarketInventory.Domain.Entities;

namespace MarketInventory.Infrastructure.Repositories.Interfaces
{
    public interface IUrunRepository : IGenericRepository<Urun>
    {
        // Urun'e özel ekstra metotlar buraya yazılır

        Task<Urun?> GetUrunWithFiyatlarAsync(int id);
        Task<Urun?> GetUrunWithBarkodlarAsync(int id);
        Task<Urun?> GetUrunWithStokHareketleriAsync(int id);
        Task<Urun?> GetUrunWithAllAsync(int id); // tüm navigation'lar dahil
    }
}