using MarketInventory.Application.Interfaces;
using MarketInventory.Application.Services.Interfaces;
using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Repositories.Interfaces;

namespace MarketInventory.Application.Services
{
    public class UrunService : GenericService<Urun>, IUrunService
    {
        private readonly IUrunRepository _urunRepository;
        private readonly IStokHareketiRepository _stokHareketiRepository;

        public UrunService(IUrunRepository urunRepository, IStokHareketiRepository stokHareketiRepository) : base(urunRepository)
        {
            _urunRepository = urunRepository;
            _stokHareketiRepository = stokHareketiRepository;
        }

        public async Task<Urun?> GetUrunWithAllAsync(int id)
        {
            return await _urunRepository.GetUrunWithAllAsync(id);
        }

        public async Task<IEnumerable<Urun>> SearchAsync(string? name, string? kategori, string? marka)
        {
            var all = await _urunRepository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(name))
                all = all.Where(u => u.Ad.Contains(name, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(kategori))
                all = all.Where(u => u.Tur != null && u.Tur.Contains(kategori, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(marka))
                all = all.Where(u => u.Tur != null && u.Tur.Contains(marka, StringComparison.OrdinalIgnoreCase));

            return all;
        }


        public async Task<int> GetStokMiktariAsync(int urunId)
        {
            var stokHareketleri = await _stokHareketiRepository.GetWithUrunAsync();

            var giris = stokHareketleri
                .Where(sh => sh.UrunId == urunId && sh.HareketTuru == "Giriş")
                .Sum(sh => sh.Miktar);

            var cikis = stokHareketleri
                .Where(sh => sh.UrunId == urunId && sh.HareketTuru == "Çıkış")
                .Sum(sh => sh.Miktar);

            return giris - cikis;
        }

        public async Task<(IEnumerable<Urun> Items, int TotalCount)> GetPagedProductsAsync(int pageIndex, int pageSize)
        {
            var all = await _urunRepository.GetAllAsync();
            var totalCount = all.Count();

            var items = all
                .Skip(pageIndex * pageSize)
                .Take(pageSize);

            return (items, totalCount);
        }

        public async Task<IEnumerable<Urun>> GetRecentProductsAsync(int count)
        {
            var all = await _urunRepository.GetAllAsync();

            return all
                .OrderByDescending(u => u.KayitTarihi)
                .Take(count);
        }

        public Task<IEnumerable<Urun>> GetActiveProductsAsync()
        {
            throw new NotImplementedException();
        }
    }

}