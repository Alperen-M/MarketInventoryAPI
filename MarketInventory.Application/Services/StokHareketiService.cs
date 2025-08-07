using MarketInventory.Application.Interfaces;
using MarketInventory.Application.Services.Interfaces;
using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Repositories.Interfaces;

namespace MarketInventory.Application.Services
{
    public class StokHareketiService : GenericService<StokHareketi>, IStokHareketiService
    {
        private readonly IStokHareketiRepository _stokHareketiRepository;

        public StokHareketiService(IStokHareketiRepository repository) : base(repository)
        {
            _stokHareketiRepository = repository;
        }

        public async Task<IEnumerable<StokHareketi>> GetStokHareketleriByUrunAsync(int urunId)
        {
            var all = await _stokHareketiRepository.GetWithUrunAsync();
            return all.Where(s => s.UrunId == urunId);
        }

        public async Task<IEnumerable<StokHareketi>> GetStokHareketleriByDateRangeAsync(int urunId, DateTime startDate, DateTime endDate)
        {
            var all = await _stokHareketiRepository.GetWithUrunAsync();
            return all.Where(s => s.UrunId == urunId && s.KayitTarihi >= startDate && s.KayitTarihi <= endDate);
        }

        public async Task<IEnumerable<StokHareketi>> GetStokHareketleriByTypeAsync(int urunId, string hareketTipi)
        {
            var all = await _stokHareketiRepository.GetWithUrunAsync();
            return all.Where(s => s.UrunId == urunId && s.HareketTuru == hareketTipi);
        }

        public async Task<decimal> GetNetStokMiktariAsync(int urunId)
        {
            var all = await _stokHareketiRepository.GetWithUrunAsync();
            // Diyelim 'Giriş' stok artışı, 'Çıkış' stok azalışı
            var girisMiktari = all.Where(s => s.UrunId == urunId && s.HareketTuru == "Giriş").Sum(s => s.Miktar);
            var cikisMiktari = all.Where(s => s.UrunId == urunId && s.HareketTuru == "Çıkış").Sum(s => s.Miktar);

            return girisMiktari - cikisMiktari;
        }
    }

}