using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Data;
using MarketInventory.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MarketInventory.Infrastructure.Repositories
{
    public class BarkodRepository : GenericRepository<Barkod>, IBarkodRepository
    {
        public BarkodRepository(MarketDbContext context) : base(context)
        {
        }

        public Task DeleteAsync(Barkod barkod)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Barkod>> GetAktifBarkodlarAsync()
        {
            return await _dbSet.Where(b => b.AktifMi).ToListAsync();
        }

        public Task UpdateAsync(Barkod barkod)
        {
            throw new NotImplementedException();
        }
    }
}