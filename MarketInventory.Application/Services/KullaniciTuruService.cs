using MarketInventory.Application.Dtos;
using MarketInventory.Application.Services.Interfaces;
using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Repositories.Interfaces;

namespace MarketInventory.Application.Services
{
    public class KullaniciTuruService : IKullaniciTuruService
    {
        private readonly IGenericRepository<KullaniciTuru> _repository;

        public KullaniciTuruService(IGenericRepository<KullaniciTuru> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<KullaniciTuruDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(x => new KullaniciTuruDto
            {
                Id = x.Id,
                Ad = x.Ad
            });
        }

        public async Task<KullaniciTuruDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            return new KullaniciTuruDto
            {
                Id = entity.Id,
                Ad = entity.Ad
            };
        }

        public async Task AddAsync(CreateKullaniciTuruDto dto)
        {
            var entity = new KullaniciTuru
            {
                Ad = dto.Ad
            };
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(int id, CreateKullaniciTuruDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new Exception("Kullanıcı türü bulunamadı.");

            entity.Ad = dto.Ad;
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null)
            {
                await _repository.DeleteAsync(entity);
            }
        }

        public Task<KullaniciTuru?> GetWithUsersAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<KullaniciTuru?> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<KullaniciTuru>> GetActiveUserTypesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsNameUniqueAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}