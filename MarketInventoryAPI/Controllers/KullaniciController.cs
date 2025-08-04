using MarketInventory.Application.Interfaces;
using MarketInventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MarketInventory.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class KullaniciController : ControllerBase
{
    private readonly IKullaniciService _kullaniciService;

    public KullaniciController(IKullaniciService kullaniciService)
    {
        _kullaniciService = kullaniciService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _kullaniciService.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Kullanici kullanici) => Ok(await _kullaniciService.CreateAsync(kullanici));
}
