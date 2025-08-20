using MarketInventory.Application.Interfaces;
using MarketInventory.Application.Services.Interfaces;
using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Data;
using MarketInventory.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MarketInventory.Application.Services
{
    public class BarkodService : IBarkodService
    {
        private readonly IBarkodRepository _barkodRepository;

        public BarkodService(IBarkodRepository barkodRepository)
        {
            _barkodRepository = barkodRepository;
        }

        public async Task<IEnumerable<Barkod>> GetAllAsync()
        {
            return await _barkodRepository.GetAllAsync();
        }

        public async Task<Barkod?> GetByIdAsync(int id)
        {
            return await _barkodRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Barkod barkod)
        {
            if (await BarkodExistsAsync(barkod.Kod))
                throw new InvalidOperationException("Bu barkod zaten mevcut.");
            await _barkodRepository.AddAsync(barkod);
        }

        public async Task UpdateAsync(Barkod barkod)
        {
            await _barkodRepository.UpdateAsync(barkod);
        }

        public async Task DeleteAsync(Barkod barkod)
        {
            await _barkodRepository.DeleteAsync(barkod);
        }

        public async Task<IEnumerable<Barkod>> GetBarkodsByUrunIdAsync(int urunId)
        {
            var all = await _barkodRepository.GetAllAsync();
            return all.Where(b => b.UrunId == urunId);
        }

        public async Task<bool> BarkodExistsAsync(string barkodKodu)
        {
            var all = await _barkodRepository.GetAllAsync();
            return all.Any(b => b.Kod == barkodKodu);
        }

        public Task<IEnumerable<Barkod>> GetActiveBarkodsAsync()
        {
            throw new NotImplementedException();
        }
    }

}