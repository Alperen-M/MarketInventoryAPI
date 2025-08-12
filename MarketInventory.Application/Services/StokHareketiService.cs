using MarketInventory.Application.DTOs;
using MarketInventory.Application.Interfaces;
using MarketInventory.Application.Services.Interfaces;
using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace MarketInventory.Application.Services
{
    public class StokHareketiService : IStokHareketiService
    {
        private readonly MarketDbContext _context;

        public StokHareketiService(MarketDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StokHareketiReadDto>> GetAllAsync()
        {
            return await _context.StokHareketleri
                .Include(s => s.Urun)
                .Include(s => s.Birim)
                .Select(s => new StokHareketiReadDto
                {
                    Id = s.Id,
                    UrunId = s.UrunId,
                    UrunAdi = s.Urun != null ? s.Urun.Ad : null,
                    HareketTuru = s.HareketTuru,
                    BirimId = s.BirimId,
                    BirimAdi = s.Birim != null ? s.Birim.Ad : null,
                    Miktar = (int)s.Miktar,
                    KayitTarihi = s.KayitTarihi,
                    GuncellemeTarihi = s.GuncellemeTarihi
                })
                .ToListAsync();
        }

        public async Task<StokHareketiReadDto?> GetByIdAsync(int id)
        {
            var entity = await _context.StokHareketleri
                .Include(s => s.Urun)
                .Include(s => s.Birim)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (entity == null) return null;

            return new StokHareketiReadDto
            {
                Id = entity.Id,
                UrunId = entity.UrunId,
                UrunAdi = entity.Urun?.Ad,
                HareketTuru = entity.HareketTuru,
                BirimId = entity.BirimId,
                BirimAdi = entity.Birim?.Ad,
                Miktar = (int)entity.Miktar,
                KayitTarihi = entity.KayitTarihi,
                GuncellemeTarihi = entity.GuncellemeTarihi
            };
        }

        public async Task<StokHareketiReadDto> AddAsync(StokHareketiCreateDto dto)
        {
            var entity = new StokHareketi
            {
                UrunId = dto.UrunId,
                HareketTuru = dto.HareketTuru,
                BirimId = dto.BirimId,
                Miktar = dto.Miktar,
                CreatedById = dto.CreatedById,
                KayitTarihi = DateTime.UtcNow
            };

            _context.StokHareketleri.Add(entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.Id) ?? throw new Exception("Kayıt eklenemedi.");
        }

        public async Task<bool> UpdateAsync(int id, StokHareketiUpdateDto dto)
        {
            var entity = await _context.StokHareketleri.FindAsync(id);
            if (entity == null) return false;

            entity.UrunId = dto.UrunId;
            entity.HareketTuru = dto.HareketTuru;
            entity.BirimId = dto.BirimId;
            entity.Miktar = dto.Miktar;
            entity.CreatedById = dto.CreatedById;
            entity.GuncellemeTarihi = DateTime.UtcNow;

            _context.StokHareketleri.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.StokHareketleri.FindAsync(id);
            if (entity == null) return false;

            _context.StokHareketleri.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
