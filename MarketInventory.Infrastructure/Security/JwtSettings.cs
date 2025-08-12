using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MarketInventory.Domain.Entities;

namespace MarketInventory.Infrastructure.Security
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationMinutes { get; set; }
        public double DurationInMinutes { get; internal set; }
        public double ExpireMinutes { get; set; }
    }
}


