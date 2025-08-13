// MarketInventory.Infrastructure/Security/JwtTokenService.cs
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MarketInventory.Domain.Entities;

namespace MarketInventory.Infrastructure.Security
{
    public interface IJwtTokenService
    {
        string Generate(Kullanici user);
    }

    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSettings _opt;
        public JwtTokenService(IOptions<JwtSettings> opt) => _opt = opt.Value;

        public string Generate(Kullanici user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.KullaniciAdi),
                new Claim(ClaimTypes.Role, user.KullaniciTuru?.Ad ?? "Musteri")
            };

            var token = new JwtSecurityToken(
                issuer: _opt.Issuer,
                audience: _opt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_opt.ExpiresMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
