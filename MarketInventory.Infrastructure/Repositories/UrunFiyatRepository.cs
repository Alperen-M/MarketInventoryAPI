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

        public Task DeleteAsync(UrunFiyat entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UrunFiyat>> GetAktifFiyatlarAsync()
        {
            return await _dbSet
                .Where(f => f.AktifMi)
                .Include(f => f.Urun)
                .ToListAsync();
        }

        public Task UpdateAsync(UrunFiyat entity)
        {
            throw new NotImplementedException();
        }
    }
}