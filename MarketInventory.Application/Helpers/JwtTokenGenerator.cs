using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MarketInventory.Infrastructure.Security
{
    public class JwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateToken(Kullanici user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
<<<<<<< HEAD
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Role, user.KullaniciTuru?.Ad ?? "Admin"),
        //new Claim(ClaimTypes.Role, user.KullaniciTuru?.Ad ?? "Çalışan"),
        //new Claim(ClaimTypes.Role, user.KullaniciTuru?.Ad ?? "Müşteri"),
    };
=======
            { 
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.KullaniciTuru?.Ad ?? "Musteri"),
            new Claim(ClaimTypes.Name, user.KullaniciAdi ?? "Kullanici"),
            new Claim(ClaimTypes.Name, user.KullaniciAdi ?? "Admin")
                };
>>>>>>> 4546a8350f84b7c71c1a2f43104c47ba71078dcb

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiresMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}



