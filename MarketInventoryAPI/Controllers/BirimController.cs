using MarketInventory.Application.Interfaces;
using MarketInventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MarketInventory.API.Controllers;

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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Birim birim) => Ok(await _birimService.CreateAsync(birim));
}
