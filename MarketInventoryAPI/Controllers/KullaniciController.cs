using MarketInventory.Application.Services.Interfaces;
using MarketInventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _kullaniciService.GetByIdAsync(id);
        return item == null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Kullanici kullanici)
        => Ok(await _kullaniciService.CreateAsync(kullanici));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Kullanici kullanici)
    {
        if (id != kullanici.Id) return BadRequest();
        await _kullaniciService.UpdateAsync(kullanici);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var kullanici = await _kullaniciService.GetByIdAsync(id);
        if (kullanici == null) return NotFound();
        await _kullaniciService.DeleteAsync(kullanici);
        return NoContent();
    }
}
