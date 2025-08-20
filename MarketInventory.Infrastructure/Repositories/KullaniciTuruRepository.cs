using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Data;
using MarketInventory.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MarketInventory.Infrastructure.Repositories
{
    public class KullaniciTuruRepository : GenericRepository<KullaniciTuru>, IKullaniciTuruRepository
    {
        public KullaniciTuruRepository(MarketDbContext context) : base(context)
        {
        }

        public async Task<KullaniciTuru?> GetWithUsersAsync(int id)
        {
            return await _dbSet
                .Include(kt => kt.Kullanicilar)
                .FirstOrDefaultAsync(kt => kt.Id == id);
        }
    }
}