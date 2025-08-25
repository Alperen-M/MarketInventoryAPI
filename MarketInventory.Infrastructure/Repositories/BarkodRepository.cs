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

        public async Task<bool> BarkodExistsAsync(string barkodKod)
        {
            // Veritabanında belirtilen barkod koduna sahip herhangi bir öğenin olup olmadığını kontrol eder.
            // Bu metot, AddAsync metodunda kullanılacak iş mantığını destekler.
            return await _dbSet.AnyAsync(b => b.Kod == barkodKod);
        }

        public Task DeleteAsync(Barkod barkod)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Barkod>> GetAktifBarkodlarAsync()
        {
            // Sadece aktif olan barkodları filtreler ve asenkron olarak döner.
            return await _dbSet.Where(b => b.AktifMi).ToListAsync();
        }

        public Task UpdateAsync(Barkod barkod)
        {
            throw new NotImplementedException();
        }
    }
}