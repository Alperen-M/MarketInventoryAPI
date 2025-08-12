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

        public async Task<IEnumerable<Birim>> GetActiveBirimlerAsync()
        {
            var all = await _repository.GetAllAsync();
            return all.Where(b => b.Carpan > 0); // Örnek: aktiflik böyle kontrol edilebilir
        }

        public async Task<Birim?> GetByNameAsync(string name)
        {
            var all = await _repository.GetAllAsync();
            return all.FirstOrDefault(b => b.Ad.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<bool> IsNameUniqueAsync(string name)
        {
            var all = await _repository.GetAllAsync();
            return !all.Any(b => b.Ad.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task AddAsync(Birim birim)
        {
            if (!await IsNameUniqueAsync(birim.Ad))
                throw new InvalidOperationException("Birim adı zaten mevcut.");

            await _repository.AddAsync(birim);
            // SaveChanges async çağrısı repository'de veya UnitOfWork'te olmalı.
        }

        public async Task UpdateAsync(Birim birim)
        {
            var existing = await GetByIdAsync(birim.Id);
            if (existing == null)
                throw new KeyNotFoundException("Birim bulunamadı.");

            if (!await IsNameUniqueAsync(birim.Ad) && existing.Ad != birim.Ad)
                throw new InvalidOperationException("Birim adı zaten mevcut.");

            existing.Ad = birim.Ad;
            existing.Carpan = birim.Carpan;
            await _repository.UpdateAsync(existing);
        }
    }


}
