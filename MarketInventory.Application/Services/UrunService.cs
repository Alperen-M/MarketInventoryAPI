using MarketInventory.Application.Interfaces;
using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketInventory.Application.Services
{
    public class UrunService : IUrunService
    {
        private readonly IUrunRepository _repository;

        public UrunService(IUrunRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Urun>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Urun?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Urun> CreateAsync(Urun urun)
        {
            return await _repository.CreateAsync(urun);
        }

        public async Task<bool> UpdateAsync(Urun urun)
        {
            return await _repository.UpdateAsync(urun);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
