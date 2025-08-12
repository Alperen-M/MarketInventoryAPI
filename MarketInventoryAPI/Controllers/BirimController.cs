using MarketInventory.Application.Dtos.Barkod;
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
    public async Task<IActionResult> Create([FromBody] BirimCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var birim = new Birim
        {
            Ad = dto.Ad,
            Carpan = dto.Carpan,

        };

        try
        {
            await _birimService.AddAsync(birim);
            return CreatedAtAction(nameof(GetById), new { id = birim.Id }, birim);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] BirimUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (id != dto.Id) return BadRequest();

        var birim = new Birim
        {
            Id = dto.Id,
            Ad = dto.Ad,
            Carpan = dto.Carpan
        };

        try
        {
            await _birimService.UpdateAsync(birim);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
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
