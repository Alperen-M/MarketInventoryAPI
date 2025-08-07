using MarketInventory.Application.Dtos;
using MarketInventory.Domain.Entities;
using MarketInventory.Infrastructure.Repositories.Interfaces;
using MarketInventory.Infrastructure.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IKullaniciRepository _userRepo;
    private readonly JwtTokenGenerator _tokenGenerator;

    public AuthController(IKullaniciRepository userRepo, JwtTokenGenerator tokenGenerator)
    {
        _userRepo = userRepo;
        _tokenGenerator = tokenGenerator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        var isLoginValid = await _userRepo.GetLoginInfoAsync(login.KullaniciAdi , login.Password);
      

        if (isLoginValid == null)
        
            return Unauthorized("Geçersiz giriş");

        var token = _tokenGenerator.GenerateToken(new Kullanici()
        {
             KullaniciAdi = login.KullaniciAdi,
             Sifre = login.Password
        });
        
        return Ok(new { token });
    }
}
