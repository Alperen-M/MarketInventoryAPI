using MarketInventory.Domain.Entities;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MarketInventory.Infrastructure.Security
{
    public class JwtTokenGenerator : IJwtTokenService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateToken(Kullanici user)
        {
            //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //    var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            //    new Claim(ClaimTypes.Role, user.KullaniciTuru.Ad)
            //};

            //    var token = new JwtSecurityToken(
            //        issuer: _jwtSettings.Issuer,
            //        audience: _jwtSettings.Audience,
            //        claims: claims,
            //        expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiresMinutes),
            //        signingCredentials: credentials
            //    );

            //    return new JwtSecurityTokenHandler().WriteToken(token);
            var claims = new[]
           {
                   new Claim(JwtRegisteredClaimNames.Name, "username"),
                      new Claim("roles", "username"),
            new Claim(JwtRegisteredClaimNames.Sub, "username"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bAafd@A7d9#@F4*V!LHZs#ebKQrkE6pad2f3kj34c3dXy@"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static ClaimsIdentity GenerateClaims()
        {
            var ci = new ClaimsIdentity();
            ci.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
            return ci;
        }
    }

}



