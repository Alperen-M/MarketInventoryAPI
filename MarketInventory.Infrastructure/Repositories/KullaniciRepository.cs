using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Data;
using MarketInventory.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MarketInventory.Infrastructure.Repositories
{
    public class KullaniciRepository : GenericRepository<Kullanici>, IKullaniciRepository
    {
        public KullaniciRepository(MarketDbContext context) : base(context)
        {
        }

        public async Task<Kullanici?> GetWithTypeAsync(int id)
        {
            return await _dbSet
                .Include(k => k.KullaniciTuru)
                .FirstOrDefaultAsync(k => k.Id == id);
        }
    }
}
