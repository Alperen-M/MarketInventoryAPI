using MarketInventory.Domain.Entities;

namespace MarketInventory.Infrastructure.Security
{
    public interface ITokenService
    {
        string GenerateToken(Kullanici user);
    }
}
