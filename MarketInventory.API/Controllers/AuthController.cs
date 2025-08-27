using MarketInventory.Application.Dtos;
using MarketInventory.Application.Services.Interfaces;
using MarketInventory.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IKullaniciService _kullaniciService;
    
    private readonly IJwtTokenService _jwtTokenService;

    public AuthController(IKullaniciService kullaniciService, IJwtTokenService jwtTokenService)
    {
        _kullaniciService = kullaniciService;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        // KRİTİK DÜZELTME: LoginAsync metodunu doğru parametrelerle çağırın
        var kullanici = await _kullaniciService.LoginAsync(loginDto.KullaniciAdi, loginDto.Password);

       
        if (kullanici == null)
        {
            return Unauthorized("Geçersiz kullanıcı adı veya şifre.");
        }

        // Token oluşturma için arayüzü kullanın
        var token = _jwtTokenService.GenerateToken(kullanici);

        return Ok(new { token });
    }
}
