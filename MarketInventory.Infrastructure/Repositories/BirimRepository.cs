using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Data;
using MarketInventory.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MarketInventory.Infrastructure.Repositories
{
    public class BirimRepository : GenericRepository<Birim>, IBirimRepository
    {
        public BirimRepository(MarketDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Birim>> GetAllWithUrunAsync()
        {
            return await _dbSet
             
                .ToListAsync();
        }
    }
}
