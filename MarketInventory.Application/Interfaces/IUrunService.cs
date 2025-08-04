using MarketInventory.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketInventory.Application.Interfaces
{
    public interface IUrunService
    {
        Task<IEnumerable<Urun>> GetAllAsync();
        Task<Urun?> GetByIdAsync(int id);
        Task<Urun> CreateAsync(Urun urun);
        Task<bool> UpdateAsync(Urun urun);
        Task<bool> DeleteAsync(int id);
    }
}
