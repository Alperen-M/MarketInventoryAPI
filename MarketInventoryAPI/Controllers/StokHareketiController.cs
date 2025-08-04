using MarketInventory.Application.Interfaces;
using MarketInventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MarketInventory.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StokHareketiController : ControllerBase
{
    private readonly IStokHareketiService _stokService;

    public StokHareketiController(IStokHareketiService stokService)
    {
        _stokService = stokService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _stokService.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] StokHareketi hareket) => Ok(await _stokService.CreateAsync(hareket));
}
