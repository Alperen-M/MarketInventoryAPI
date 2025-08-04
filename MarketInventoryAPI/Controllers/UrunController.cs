using MarketInventory.Application.Interfaces;
using MarketInventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MarketInventory.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UrunController : ControllerBase
{
    private readonly IUrunService _urunService;

    public UrunController(IUrunService urunService)
    {
        _urunService = urunService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _urunService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) => Ok(await _urunService.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Urun urun) => Ok(await _urunService.CreateAsync(urun));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Urun urun)
    {
        urun.Id = id;
        return Ok(await _urunService.UpdateAsync(urun));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => Ok(await _urunService.DeleteAsync(id));
}
