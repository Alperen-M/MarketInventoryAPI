using MarketInventory.Application.Interfaces;
using MarketInventory.Domain.Entities;

namespace MarketInventory.Application.Services.Interfaces
{
    public interface IKullaniciTuruService : IGenericService<KullaniciTuru>
    {
        Task<KullaniciTuru?> GetWithUsersAsync(int id);
        Task<KullaniciTuru?> GetByNameAsync(string name);
        Task<IEnumerable<KullaniciTuru>> GetActiveUserTypesAsync();
        Task<bool> IsNameUniqueAsync(string name);
    }


}
