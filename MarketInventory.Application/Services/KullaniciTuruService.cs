using MarketInventory.Application.Interfaces;
using MarketInventory.Application.Services.Interfaces;
using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Repositories.Interfaces;

namespace MarketInventory.Application.Services
{
    public class KullaniciTuruService : GenericService<KullaniciTuru>, IKullaniciTuruService
    {
        public KullaniciTuruService(IGenericRepository<KullaniciTuru> repository) : base(repository)
        {
        }
    }
}