using MarketInventory.Application.DTOs.UrunFiyat;
using MarketInventory.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UrunFiyatController : ControllerBase
{
    private readonly IUrunFiyatService _service;

    public UrunFiyatController(IUrunFiyatService service)
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
    public async Task<IActionResult> Create([FromBody] CreateUrunFiyatDto dto)
    {
        await _service.CreateAsync(dto);
        return StatusCode(201);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUrunFiyatDto dto)
    {
        if (id != dto.Id) return BadRequest();
        await _service.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("urun/{urunId}")]
    public async Task<IActionResult> GetByUrunId(int urunId) =>
        Ok(await _service.GetFiyatlarByUrunIdAsync(urunId));

    [HttpGet("aktif")]
    public async Task<IActionResult> GetAktifFiyatlar() =>
        Ok(await _service.GetAktifFiyatlarAsync());

    [HttpGet("enguncel/{urunId}")]
    public async Task<IActionResult> GetEnGuncelFiyat(int urunId) =>
        Ok(await _service.GetEnGuncelFiyatAsync(urunId));

    [HttpGet("price-range")]
    public async Task<IActionResult> GetByPriceRange(decimal minPrice, decimal maxPrice) =>
        Ok(await _service.GetFiyatlarByPriceRangeAsync(minPrice, maxPrice));

    [HttpGet("date-range")]
    public async Task<IActionResult> GetByDateRange(int urunId, DateTime startDate, DateTime endDate) =>
        Ok(await _service.GetFiyatlarByDateRangeAsync(urunId, startDate, endDate));
}
