using MarketInventory.Domain.Entities;

namespace MarketInventory.Infrastructure.Repositories.Interfaces
{
    public interface IKullaniciTuruRepository : IGenericRepository<KullaniciTuru>
    {
        Task<KullaniciTuru?> GetWithUsersAsync(int id);
    }
}