using MarketInventory.Application.Services.Interfaces;
using MarketInventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class BirimController : ControllerBase
{
    private readonly IBirimService _birimService;

    public BirimController(IBirimService birimService)
    {
        _birimService = birimService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _birimService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _birimService.GetByIdAsync(id);
        return item == null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Birim birim)
        => Ok(await _birimService.CreateAsync(birim));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Birim birim)
    {
        if (id != birim.Id) return BadRequest();
        await _birimService.UpdateAsync(birim);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var birim = await _birimService.GetByIdAsync(id);
        if (birim == null) return NotFound();
        await _birimService.DeleteAsync(birim);
        return NoContent();
    }
}