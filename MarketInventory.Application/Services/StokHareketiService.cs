using MarketInventory.Application.Interfaces;
using MarketInventory.Application.Services.Interfaces;
using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Repositories.Interfaces;

namespace MarketInventory.Application.Services
{
    public class StokHareketiService : GenericService<StokHareketi>, IStokHareketiService
    {
        public StokHareketiService(IGenericRepository<StokHareketi> repository) : base(repository)
        {
        }
    }
}