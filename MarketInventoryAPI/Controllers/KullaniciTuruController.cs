using MarketInventory.Application.Interfaces;
using MarketInventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MarketInventory.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class KullaniciTuruController : ControllerBase
{
    private readonly IKullaniciTuruService _kullaniciTuruService;

    public KullaniciTuruController(IKullaniciTuruService kullaniciTuruService)
    {
        _kullaniciTuruService = kullaniciTuruService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _kullaniciTuruService.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] KullaniciTuru tur) => Ok(await _kullaniciTuruService.CreateAsync(tur));
}
