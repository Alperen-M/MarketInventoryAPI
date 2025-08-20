using MarketInventory.Application.Interfaces;
using MarketInventory.Domain.Entities;

namespace MarketInventory.Application.Services.Interfaces
{
    public interface IBirimService : IGenericService<Birim>
    {
        Task<Birim?> GetByNameAsync(string name);
        Task<bool> IsNameUniqueAsync(string name);
        Task<IEnumerable<Birim>> GetActiveBirimlerAsync();
    }

}