using MarketInventory.Application.Services.Interfaces;
using MarketInventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class KullaniciTuruController : ControllerBase
{
    private readonly IKullaniciTuruService _service;

    public KullaniciTuruController(IKullaniciTuruService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        return item == null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] KullaniciTuru entity)
        => Ok(await _service.CreateAsync(entity));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] KullaniciTuru entity)
    {
        if (id != entity.Id) return BadRequest();
        await _service.UpdateAsync(entity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        await _service.DeleteAsync(item);
        return NoContent();
    }
}
