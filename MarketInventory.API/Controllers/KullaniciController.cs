using MarketInventory.Application.Services.Interfaces;
using MarketInventory.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MarketInventory.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciController : ControllerBase
    {
        private readonly IKullaniciService _kullaniciService;

        public KullaniciController(IKullaniciService kullaniciService)
        {
            _kullaniciService = kullaniciService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var kullanicilar = await _kullaniciService.GetAllAsync();
            return Ok(kullanicilar);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var kullanici = await _kullaniciService.GetByIdAsync(id);
            if (kullanici == null)
                return NotFound();
            return Ok(kullanici);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Kullanici kullanici)
        {
            await _kullaniciService.AddAsync(kullanici);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Kullanici kullanici)
        {
            if (id != kullanici.Id)
                return BadRequest();

            await _kullaniciService.UpdateAsync(kullanici);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var kullanici = await _kullaniciService.GetByIdAsync(id);
            if (kullanici == null)
                return NotFound();

            await _kullaniciService.DeleteAsync(kullanici);
            return NoContent();
        }
    }
}