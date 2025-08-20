using MarketInventory.Application.Interfaces;
using MarketInventory.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketInventory.Application.Services.Interfaces
{
    public interface IBarkodService : IGenericService<Barkod>
    {
        Task<IEnumerable<Barkod>> GetBarkodsByUrunIdAsync(int urunId);
        Task<bool> BarkodExistsAsync(string barkodKodu);
        Task<IEnumerable<Barkod>> GetActiveBarkodsAsync();
    }
}