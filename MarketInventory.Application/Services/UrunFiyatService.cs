using MarketInventory.Application.Interfaces;
using MarketInventory.Application.Services.Interfaces;
using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Repositories.Interfaces;

namespace MarketInventory.Application.Services
{
    public class UrunFiyatService : GenericService<UrunFiyat>, IUrunFiyatService
    {
        public UrunFiyatService(IGenericRepository<UrunFiyat> repository) : base(repository)
        {
        }
    }
}