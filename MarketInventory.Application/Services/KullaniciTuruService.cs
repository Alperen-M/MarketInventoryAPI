using MarketInventory.Application.Interfaces;
using MarketInventory.Application.Services.Interfaces;
using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Repositories.Interfaces;

namespace MarketInventory.Application.Services
{
    public class KullaniciTuruService : GenericService<KullaniciTuru>, IKullaniciTuruService
    {
        private readonly IGenericRepository<KullaniciTuru> _repository;

        public KullaniciTuruService(IGenericRepository<KullaniciTuru> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<KullaniciTuru?> GetWithUsersAsync(int id)
        {
            // Eğer repositoryde özel metod yoksa tüm kayıtları çekip filtrelemek performans için uygun değil
            // Bu durumda repositoryde Include ile getirme metodu yazmalısın.
            var all = await _repository.GetAllAsync();
            return all.FirstOrDefault(kt => kt.Id == id);
        }

        public async Task<KullaniciTuru?> GetByNameAsync(string name)
        {
            var all = await _repository.GetAllAsync();
            return all.FirstOrDefault(kt => kt.Ad.ToLower() == name.ToLower());
        }

     
        public async Task<bool> IsNameUniqueAsync(string name)
        {
            var all = await _repository.GetAllAsync();
            return !all.Any(kt => kt.Ad.ToLower() == name.ToLower());
        }

        public Task<IEnumerable<KullaniciTuru>> GetActiveUserTypesAsync()
        {
            throw new NotImplementedException();
        }
    }

}