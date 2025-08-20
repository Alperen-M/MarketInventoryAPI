using MarketInventory.Application.DTOs.UrunFiyat;
using MarketInventory.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    [AllowAnonymous]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        return item == null ? NotFound() : Ok(item);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Çalışan")]
    public async Task<IActionResult> Create([FromBody] CreateUrunFiyatDto dto)
    {
        await _service.CreateAsync(dto);
        return StatusCode(201);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Çalışan")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUrunFiyatDto dto)
    {
        if (id != dto.Id) return BadRequest();
        await _service.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("urun/{urunId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByUrunId(int urunId) =>
        Ok(await _service.GetFiyatlarByUrunIdAsync(urunId));

    [HttpGet("aktif")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAktifFiyatlar() =>
        Ok(await _service.GetAktifFiyatlarAsync());

    [HttpGet("enguncel/{urunId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetEnGuncelFiyat(int urunId) =>
        Ok(await _service.GetEnGuncelFiyatAsync(urunId));

    [HttpGet("price-range")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByPriceRange(decimal minPrice, decimal maxPrice) =>
        Ok(await _service.GetFiyatlarByPriceRangeAsync(minPrice, maxPrice));

    [HttpGet("date-range")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByDateRange(int urunId, DateTime startDate, DateTime endDate) =>
        Ok(await _service.GetFiyatlarByDateRangeAsync(urunId, startDate, endDate));
}