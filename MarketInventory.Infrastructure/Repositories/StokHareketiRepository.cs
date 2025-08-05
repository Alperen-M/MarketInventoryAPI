using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Data;
using MarketInventory.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MarketInventory.Infrastructure.Repositories
{
    public class StokHareketiRepository : GenericRepository<StokHareketi>, IStokHareketiRepository
    {
        public StokHareketiRepository(MarketDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<StokHareketi>> GetWithUrunAsync()
        {
            return await _dbSet
                .Include(s => s.Urun)
                .Include(s => s.Birim)
                .ToListAsync();
        }
    }
}
