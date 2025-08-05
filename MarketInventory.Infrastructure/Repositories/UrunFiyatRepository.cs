using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Data;
using MarketInventory.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MarketInventory.Infrastructure.Repositories
{
    public class UrunFiyatRepository : GenericRepository<UrunFiyat>, IUrunFiyatRepository
    {
        public UrunFiyatRepository(MarketDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<UrunFiyat>> GetAktifFiyatlarAsync()
        {
            return await _dbSet
                .Where(f => f.AktifMi)
                .Include(f => f.Urun)
                .ToListAsync();
        }
    }
}

