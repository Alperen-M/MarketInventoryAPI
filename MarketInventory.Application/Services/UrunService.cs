using MarketInventory.Application.Interfaces;
using MarketInventory.Application.Services.Interfaces;
using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Repositories.Interfaces;

namespace MarketInventory.Application.Services
{
    public class UrunService : GenericService<Urun>, IUrunService
    {
        public UrunService(IGenericRepository<Urun> repository) : base(repository)
        {
        }
    }
}