using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Data;
using MarketInventory.Infrastructure.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly MarketDbContext _ctx;
        private readonly IJwtTokenService _jwt;

        public AuthController(MarketDbContext ctx, IJwtTokenService jwt)
        {
            _ctx = ctx;
            _jwt = jwt;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var user = await _ctx.Kullanicilar
                .Include(k => k.KullaniciTuru)
                .FirstOrDefaultAsync(x => x.KullaniciAdi == req.KullaniciAdi && x.Sifre == req.Sifre);

            if (user == null) return Unauthorized("Kullanıcı adı veya şifre yanlış.");

            var token = _jwt.Generate(user);
            return Ok(new { token, role = user.KullaniciTuru?.Ad });
        }
    }

    public class LoginRequest
    {
        public string KullaniciAdi { get; set; } = string.Empty;
        public string Sifre { get; set; } = string.Empty;
    }
}
