using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Data;
using MarketInventory.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MarketInventory.Infrastructure.Repositories
{
    public class UrunRepository : GenericRepository<Urun>, IUrunRepository
    {
        private readonly MarketDbContext _context;

        public UrunRepository(MarketDbContext context) : base(context)
        {
            _context = context;
        }

        // Urun ve fiyatlar
        public async Task<Urun?> GetUrunWithFiyatlarAsync(int id)
        {
            return await _context.Urunler
                .Include(u => u.Fiyatlar)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        // Urun ve barkodlar
        public async Task<Urun?> GetUrunWithBarkodlarAsync(int id)
        {
            return await _context.Urunler
                .Include(u => u.Barkodlar)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        // Urun ve stok hareketleri
        public async Task<Urun?> GetUrunWithStokHareketleriAsync(int id)
        {
            return await _context.Urunler
                .Include(u => u.StokHareketleri)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        // Hepsi bir arada
        public async Task<Urun?> GetUrunWithAllAsync(int id)
        {
            return await _context.Urunler
                .Include(u => u.Barkodlar)
                .Include(u => u.Fiyatlar)
                .Include(u => u.StokHareketleri)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}