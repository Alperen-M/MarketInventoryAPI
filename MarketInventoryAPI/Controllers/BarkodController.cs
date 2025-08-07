using MarketInventory.Application.Services.Interfaces;
using MarketInventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MarketInventory.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BarkodController : ControllerBase
{
    private readonly IBarkodService _barkodService;

    public BarkodController(IBarkodService barkodService)
    {
        _barkodService = barkodService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _barkodService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _barkodService.GetByIdAsync(id);
        
        return item == null ? NotFound() : Ok(item);
    }

    [HttpPost]
  
    public async Task<IActionResult> Create([FromBody] Barkod barkod)
    {
        await _barkodService.AddAsync(barkod);
        
        return NoContent();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Barkod barkod)
    {
        if (id != barkod.Id) return BadRequest();
        
        await _barkodService.UpdateAsync(barkod);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var barkod = await _barkodService.GetByIdAsync(id);
        
        if (barkod == null) return NotFound();
        
        await _barkodService.DeleteAsync(barkod);
        
        return NoContent();
    }
}
