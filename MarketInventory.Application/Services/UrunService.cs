using MarketInventory.Application.DTOs;
using MarketInventory.Application.Interfaces;
using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

using System;

namespace MarketInventory.Application.Services
{
    public class UrunService : IUrunService
    {
        private readonly MarketDbContext _context;

        public UrunService(MarketDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UrunReadDto>> GetAllAsync()
        {
            return await _context.Urunler
                .Select(u => new UrunReadDto
                {
                    Id = u.Id,
                    Ad = u.Ad,
                    Tur = u.Tur,
                    BirimId = u.BirimId,
                    BirimAdi = u.Birim != null ? u.Birim.Ad : null,
                    KayitTarihi = u.KayitTarihi,
                    GuncellemeTarihi = u.GuncellemeTarihi
                })
                .ToListAsync();
        }

        public async Task<UrunReadDto?> GetByIdAsync(int id)
        {
            var entity = await _context.Urunler
                .Include(u => u.Birim)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (entity == null) return null;

            return new UrunReadDto
            {
                Id = entity.Id,
                Ad = entity.Ad,
                Tur = entity.Tur,
                BirimId = entity.Birim?.Id ?? entity.BirimId,
                BirimAdi = entity.Birim?.Ad,
                KayitTarihi = entity.KayitTarihi,
                GuncellemeTarihi = entity.GuncellemeTarihi
            };
        }

        public async Task<UrunReadDto> AddAsync(UrunCreateDto dto)
        {
            var entity = new Urun
            {
                Ad = dto.Ad,
                Tur = dto.Tur,
                BirimId = dto.BirimId,
                CreatedById = dto.CreatedById,
                KayitTarihi = DateTime.UtcNow
            };

            _context.Urunler.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }
        private UrunReadDto MapToDto(Urun entity)
        {
            var birimAdi = _context.Birimler.FirstOrDefault(b => b.Id == entity.BirimId)?.Ad;

            return new UrunReadDto
            {
                Id = entity.Id,
                Ad = entity.Ad,
                Tur = entity.Tur,
                BirimId = entity.BirimId,
                BirimAdi = birimAdi ?? string.Empty 
            };
        }
        public async Task<bool> UpdateAsync(int id, UrunUpdateDto dto)
        {
            var entity = await _context.Urunler.FindAsync(id);
            if (entity == null) return false;

            entity.Ad = dto.Ad;
            entity.Tur = dto.Tur;
            entity.BirimId = dto.BirimId;
            entity.CreatedById = dto.CreatedById;
            entity.GuncellemeTarihi = DateTime.UtcNow;

            _context.Urunler.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Urunler.FindAsync(id);
            if (entity == null) return false;

            _context.Urunler.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}