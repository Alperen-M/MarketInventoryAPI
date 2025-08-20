using MarketInventory.Domain.Entities;

namespace MarketInventory.Infrastructure.Repositories.Interfaces
{
    public interface IKullaniciRepository : IGenericRepository<Kullanici>
    {
        Task<Kullanici?> GetWithTypeAsync(int id);
        Task<bool> GetLoginInfoAsync(string KullaniciAdi, string Sifre);
    }
}