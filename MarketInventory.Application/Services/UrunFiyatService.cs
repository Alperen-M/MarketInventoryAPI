using MarketInventory.Application.DTOs.UrunFiyat;
using MarketInventory.Application.Services.Interfaces;
using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Repositories.Interfaces;

namespace MarketInventory.Application.Services
{
    public class UrunFiyatService : IUrunFiyatService
    {
        private readonly IUrunFiyatRepository _repo;

        public UrunFiyatService(IUrunFiyatRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<UrunFiyatDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(MapToDto);
        }

        public async Task<UrunFiyatDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : MapToDto(entity);
        }

        public async Task CreateAsync(CreateUrunFiyatDto dto)
        {
            var entity = new UrunFiyat
            {
                AktifMi = dto.AktifMi,
                BaslangicTarihi = dto.BaslangicTarihi,
                SonTarih = dto.SonTarih,
                Fiyat = dto.Fiyat,
                UrunId = (int)dto.UrunId
            };
            await _repo.AddAsync(entity);
        }

        public async Task UpdateAsync(UpdateUrunFiyatDto dto)
        {
            var entity = await _repo.GetByIdAsync(dto.Id);
            if (entity == null) throw new Exception("Kayıt bulunamadı");

            entity.AktifMi = dto.AktifMi;
            entity.BaslangicTarihi = dto.BaslangicTarihi;
            entity.SonTarih = dto.SonTarih;
            entity.Fiyat = dto.Fiyat;
            entity.UrunId = (int)dto.UrunId;

            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity != null)
                await _repo.DeleteAsync(entity);
        }

        public async Task<IEnumerable<UrunFiyatDto>> GetFiyatlarByUrunIdAsync(int urunId)
        {
            var all = await _repo.GetAllAsync();
            return all.Where(f => f.UrunId == urunId).Select(MapToDto);
        }

        public async Task<IEnumerable<UrunFiyatDto>> GetAktifFiyatlarAsync()
        {
            var list = await _repo.GetAktifFiyatlarAsync();
            return list.Select(MapToDto);
        }

        public async Task<UrunFiyatDto?> GetEnGuncelFiyatAsync(int urunId)
        {
            var all = await _repo.GetAllAsync();
            var entity = all
                .Where(f => f.UrunId == urunId && f.AktifMi)
                .OrderByDescending(f => f.BaslangicTarihi)
                .FirstOrDefault();

            return entity == null ? null : MapToDto(entity);
        }

        public async Task<IEnumerable<UrunFiyatDto>> GetFiyatlarByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var all = await _repo.GetAllAsync();
            return all.Where(f => f.Fiyat >= minPrice && f.Fiyat <= maxPrice)
                      .Select(MapToDto);
        }

        public async Task<IEnumerable<UrunFiyatDto>> GetFiyatlarByDateRangeAsync(int urunId, DateTime startDate, DateTime endDate)
        {
            var all = await _repo.GetAllAsync();
            return all.Where(f => f.UrunId == urunId && f.BaslangicTarihi >= startDate && f.SonTarih <= endDate)
                      .Select(MapToDto);
        }

        private UrunFiyatDto MapToDto(UrunFiyat entity)
        {
            return new UrunFiyatDto
            {
                Id = entity.Id,
                AktifMi = entity.AktifMi,
                BaslangicTarihi = entity.BaslangicTarihi,
                // Hata veren casting işlemi kaldırıldı
                SonTarih = entity.SonTarih,
                Fiyat = entity.Fiyat,
                UrunId = entity.UrunId,
                UrunAdi = entity.Urun?.Ad
            };
        }
    }
}