using MarketInventory.Application.Interfaces;
using MarketInventory.Application.Services.Interfaces;
using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketInventory.Application.Services
{
    public class KullaniciService : GenericService<Kullanici>, IKullaniciService
    {
        private readonly IGenericRepository<Kullanici> _repository;

        public KullaniciService(IGenericRepository<Kullanici> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Kullanici?> GetKullaniciWithDetayAsync(int id)
        {
            var all = await _repository.GetAllAsync();
            return all.FirstOrDefault(k => k.Id == id);
        }

        public async Task<Kullanici?> GetByEmailAsync(string email)
        {
            var all = await _repository.GetAllAsync();
            return all.FirstOrDefault(k => k.KullaniciAdi.ToLower() == email.ToLower());
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            var all = await _repository.GetAllAsync();
            return !all.Any(k => k.KullaniciAdi.ToLower() == email.ToLower());
        }

        public async Task<Kullanici?> LoginAsync(string email, string password)
        {
            var all = await _repository.GetAllAsync();
            return all.FirstOrDefault(k =>
                k.KullaniciAdi.ToLower() == email.ToLower() &&
                k.Sifre == password);
        }

        public async Task<IEnumerable<Kullanici>> GetActiveKullanicilarAsync()
        {
            var all = await _repository.GetAllAsync();
            return all.Where(k => k.SilinmeTarihi == null);
        }

       
    }
}