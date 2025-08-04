using MarketInventory.Application.Interfaces;
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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Barkod barkod) => Ok(await _barkodService.CreateAsync(barkod));
}
