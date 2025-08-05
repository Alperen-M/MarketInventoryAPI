using MarketInventory.Application.Interfaces;
using MarketInventory.Domain.Entities;

namespace MarketInventory.Application.Services.Interfaces
{
    public interface IStokHareketiService : IGenericService<StokHareketi>
    {
        Task<IEnumerable<StokHareketi>> GetStokHareketleriByUrunAsync(int urunId);
    }
}
