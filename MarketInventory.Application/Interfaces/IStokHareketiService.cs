using MarketInventory.Application.Interfaces;
using MarketInventory.Domain.Entities;

namespace MarketInventory.Application.Services.Interfaces
{
    public interface IStokHareketiService : IGenericService<StokHareketi>
    {
        Task<IEnumerable<StokHareketi>> GetStokHareketleriByUrunAsync(int urunId);
        Task<IEnumerable<StokHareketi>> GetStokHareketleriByDateRangeAsync(int urunId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<StokHareketi>> GetStokHareketleriByTypeAsync(int urunId, string hareketTipi); // örn: "Giriş" veya "Çıkış"
        Task<decimal> GetNetStokMiktariAsync(int urunId); // Toplam stok
    }


}
