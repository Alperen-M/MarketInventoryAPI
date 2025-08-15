using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MarketInventory.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MarketInventory.Infrastructure.Security
{
    public interface IJwtTokenService
    {
        string GenerateToken(Kullanici user);
    }

    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSettings _jwt;

        public JwtTokenService(IOptions<JwtSettings> jwt)
        {
            _jwt = jwt.Value;
        }

        public string Generate(Kullanici user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // DİKKAT: Rol claim’i birebir "Admin" / "Çalışan" / "Müşteri" ile eşleşmeli.
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.KullaniciAdi ?? string.Empty),
                new Claim(ClaimTypes.Role, user.KullaniciTuru?.Ad ?? "Müşteri")
            };

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.ExpiresMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateToken(Kullanici user)
        {
            throw new NotImplementedException();
        }
    }
}
