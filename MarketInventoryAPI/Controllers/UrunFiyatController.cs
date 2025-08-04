using MarketInventory.Application.Interfaces;
using MarketInventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MarketInventory.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UrunFiyatController : ControllerBase
{
    private readonly IUrunFiyatService _fiyatService;

    public UrunFiyatController(IUrunFiyatService fiyatService)
    {
        _fiyatService = fiyatService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _fiyatService.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UrunFiyat fiyat) => Ok(await _fiyatService.CreateAsync(fiyat));
}
