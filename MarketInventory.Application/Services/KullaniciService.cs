using MarketInventory.Application.Interfaces;
using MarketInventory.Application.Services.Interfaces;
using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Data;
using MarketInventory.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketInventory.Application.Services
{
    public class KullaniciService : GenericService<Kullanici>, IKullaniciService
    {
        private readonly IGenericRepository<Kullanici> _repository;
        private readonly MarketDbContext _context;


        public KullaniciService(IGenericRepository<Kullanici> repository,MarketDbContext marketDbContext) : base(repository)
        {
            _repository = repository;
            _context = marketDbContext;
               

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
            var y = await _context.Kullanicilar
                .Where(x => x.KullaniciAdi.ToLower() == email.ToLower() && x.Sifre == password)
                .Select(x => new Kullanici
                {
                    KullaniciAdi = x.KullaniciAdi,
                    KullaniciTuru = x.KullaniciTuru,
                    Id = x.Id
                })
                .FirstOrDefaultAsync();

            return y;
        }

        public async Task<IEnumerable<Kullanici>> GetActiveKullanicilarAsync()
        {
            var all = await _repository.GetAllAsync();
            return all.Where(k => k.SilinmeTarihi == null);
        }


    }
}