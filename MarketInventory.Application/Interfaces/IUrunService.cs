using MarketInventory.Application.Interfaces;
using MarketInventory.Domain.Entities;

namespace MarketInventory.Application.Services.Interfaces
{
    public interface IUrunService : IGenericService<Urun>
    {
        Task<Urun?> GetUrunWithAllAsync(int id);
        Task<IEnumerable<Urun>> SearchAsync(string? name, string? kategori, string? marka);
        Task<IEnumerable<Urun>> GetActiveProductsAsync();
        Task<int> GetStokMiktariAsync(int urunId);
        Task<(IEnumerable<Urun> Items, int TotalCount)> GetPagedProductsAsync(int pageIndex, int pageSize);
        Task<IEnumerable<Urun>> GetRecentProductsAsync(int count);
    }


}
