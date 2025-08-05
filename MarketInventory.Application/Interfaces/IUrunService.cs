using MarketInventory.Application.Interfaces;
using MarketInventory.Domain.Entities;

namespace MarketInventory.Application.Services.Interfaces
{
    public interface IUrunService : IGenericService<Urun>
    {
        Task<Urun?> GetUrunWithAllAsync(int id);
    }
}
