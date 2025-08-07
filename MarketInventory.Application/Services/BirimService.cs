using MarketInventory.Application.Interfaces;
using MarketInventory.Application.Services.Interfaces;
using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Repositories.Interfaces;

namespace MarketInventory.Application.Services
{
    public class BirimService : GenericService<Birim>, IBirimService
    {
        private readonly IGenericRepository<Birim> _repository;

        public BirimService(IGenericRepository<Birim> repository) : base(repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Birim>> GetActiveBirimlerAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Birim?> GetByNameAsync(string name)
        {
            var all = await _repository.GetAllAsync();
            return all.FirstOrDefault(b => b.Ad.ToLower() == name.ToLower());
        }

        public async Task<bool> IsNameUniqueAsync(string name)
        {
            var all = await _repository.GetAllAsync();
            return !all.Any(b => b.Ad.ToLower() == name.ToLower());
        }

        
    }

}
