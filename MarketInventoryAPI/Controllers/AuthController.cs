using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarketInventory.Infrastructure.Data; // DbContext için

namespace MarketInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly MarketDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthController(MarketDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Kullanicilar
                .Include(k => k.KullaniciTuru) // Rol bilgisini çekiyoruz
                .FirstOrDefaultAsync(x => x.KullaniciAdi == request.KullaniciAdi && x.Sifre == request.Sifre);

            if (user == null)
                return Unauthorized("Kullanıcı adı veya şifre yanlış.");

            var token = _jwtService.GenerateToken(user);

            return Ok(new
            {
                token,
                role = user.KullaniciTuru?.Ad
            });
        }
    }

    public class LoginRequest
    {
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
    }
}
