using MarketInventory.Application.Interfaces;
using MarketInventory.Domain.Entities;

namespace MarketInventory.Application.Services.Interfaces
{
    public interface IKullaniciService : IGenericService<Kullanici>
    {
        Task<Kullanici?> GetKullaniciWithDetayAsync(int id);
    }
}
