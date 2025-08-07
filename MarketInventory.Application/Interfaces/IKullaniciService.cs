using MarketInventory.Application.Interfaces;
using MarketInventory.Domain.Entities;

namespace MarketInventory.Application.Services.Interfaces
{
    public interface IKullaniciService : IGenericService<Kullanici>
    {
        Task<Kullanici?> GetKullaniciWithDetayAsync(int id); // zaten vardı
        Task<Kullanici?> GetByEmailAsync(string email);
        Task<bool> IsEmailUniqueAsync(string email);
        Task<IEnumerable<Kullanici>> GetActiveKullanicilarAsync();
        Task<Kullanici?> LoginAsync(string email, string password);
    }


}
