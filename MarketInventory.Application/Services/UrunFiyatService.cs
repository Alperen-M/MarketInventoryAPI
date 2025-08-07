using MarketInventory.Application.Interfaces;
using MarketInventory.Application.Services.Interfaces;
using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Repositories.Interfaces;

namespace MarketInventory.Application.Services
{
    public class UrunFiyatService : GenericService<UrunFiyat>, IUrunFiyatService
    {
        private readonly IUrunFiyatRepository _urunFiyatRepository;

        public UrunFiyatService(IUrunFiyatRepository repository) : base(repository)
        {
            _urunFiyatRepository = repository;
        }

        public async Task<IEnumerable<UrunFiyat>> GetFiyatlarByUrunIdAsync(int urunId)
        {
            var all = await _urunFiyatRepository.GetAllAsync();
            return all.Where(f => f.UrunId == urunId);
        }

        public async Task<IEnumerable<UrunFiyat>> GetAktifFiyatlarAsync()
        {
            return await _urunFiyatRepository.GetAktifFiyatlarAsync();
        }

        public async Task<UrunFiyat?> GetEnGuncelFiyatAsync(int urunId)
        {
            var all = await _urunFiyatRepository.GetAllAsync();
            return all
                .Where(f => f.UrunId == urunId && f.AktifMi)
                .OrderByDescending(f => f.BaslangicTarihi)
                .FirstOrDefault();
        }

        public async Task<IEnumerable<UrunFiyat>> GetFiyatlarByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var all = await _urunFiyatRepository.GetAllAsync();
            return all.Where(f => f.Fiyat >= minPrice && f.Fiyat <= maxPrice);
        }

        public async Task<IEnumerable<UrunFiyat>> GetFiyatlarByDateRangeAsync(int urunId, DateTime startDate, DateTime endDate)
        {
            var all = await _urunFiyatRepository.GetAllAsync();
            return all.Where(f => f.UrunId == urunId && f.BaslangicTarihi >= startDate && f.SonTarih <= endDate);
        }
    }

}