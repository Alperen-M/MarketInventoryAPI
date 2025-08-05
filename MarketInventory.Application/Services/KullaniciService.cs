using MarketInventory.Application.Interfaces;
using MarketInventory.Application.Services.Interfaces;
using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Repositories.Interfaces;

namespace MarketInventory.Application.Services
{
    public class KullaniciService : GenericService<Kullanici>, IKullaniciService
    {
        public KullaniciService(IGenericRepository<Kullanici> repository) : base(repository)
        {
        }
    }
}