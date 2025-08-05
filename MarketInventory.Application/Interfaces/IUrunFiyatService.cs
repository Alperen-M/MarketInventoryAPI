using MarketInventory.Application.Interfaces;
using MarketInventory.Domain.Entities;

namespace MarketInventory.Application.Services.Interfaces
{
    public interface IUrunFiyatService : IGenericService<UrunFiyat>
    {
        Task<IEnumerable<UrunFiyat>> GetFiyatlarByUrunIdAsync(int urunId);
    }
}
