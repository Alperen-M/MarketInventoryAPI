using MarketInventory.Application.Dtos.Barkod;
using MarketInventory.Application.Services.Interfaces;
using MarketInventory.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketInventory.API.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> GetAll()
        {
            var barkods = await _barkodService.GetAllAsync();

            var result = barkods.Select(b => new BarkodDetailDto
            {
                Id = b.Id,
                Kod = b.Kod,
                UrunAdi = b.Urun?.Ad,
                BirimAdi = b.Birim?.Ad,
                AktifMi = b.AktifMi,
                KayitTarihi = b.KayitTarihi,
                GuncellemeTarihi = b.GuncellemeTarihi
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var b = await _barkodService.GetByIdAsync(id);
            if (b == null) return NotFound();

            var dto = new BarkodDetailDto
            {
                Id = b.Id,
                Kod = b.Kod,
                UrunAdi = b.Urun?.Ad,
                BirimAdi = b.Birim?.Ad,
                AktifMi = b.AktifMi,
                KayitTarihi = b.KayitTarihi,
                GuncellemeTarihi = b.GuncellemeTarihi
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BarkodCreateDto dto)
        {
            var barkod = new Barkod
            {
                Kod = dto.Kod,
                UrunId = dto.UrunId,
                BirimId = dto.UrunBirimId,
                AktifMi = dto.AktifMi,
                CreatedById = dto.CreatedById,
                UrunBirimi = dto.UrunBirimi
            };

            await _barkodService.AddAsync(barkod);

            // 201 Created + Location Header döndürelim
            return CreatedAtAction(nameof(GetById), new { id = barkod.Id }, barkod);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BarkodUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var barkod = await _barkodService.GetByIdAsync(id);
            if (barkod == null) return NotFound();

            barkod.Kod = dto.Kod;
            barkod.UrunId = dto.UrunId;
            barkod.BirimId = dto.BirimId;
            barkod.AktifMi = dto.AktifMi;
            barkod.GuncellemeTarihi = DateTime.UtcNow;

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
}
