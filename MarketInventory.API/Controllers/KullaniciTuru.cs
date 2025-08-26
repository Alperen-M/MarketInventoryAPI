using MarketInventory.Application.Dtos;
using MarketInventory.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketInventory.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciTuruController : ControllerBase
    {
        private readonly IKullaniciTuruService _service;
        public KullaniciTuruController(IKullaniciTuruService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateKullaniciTuruDto dto)
        {
            await _service.AddAsync(dto);
            return Ok("Kullanıcı türü eklendi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateKullaniciTuruDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return Ok("Kullanıcı türü güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok("Kullanıcı türü silindi.");
        }
    }
}
