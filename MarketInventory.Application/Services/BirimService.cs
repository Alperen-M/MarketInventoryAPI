using MarketInventory.Application.Interfaces;
using MarketInventory.Application.Services.Interfaces;
using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Repositories.Interfaces;

namespace MarketInventory.Application.Services
{
    public class BirimService : GenericService<Birim>, IBirimService
    {
        public BirimService(IGenericRepository<Birim> repository) : base(repository)
        {
        }
    }
}
