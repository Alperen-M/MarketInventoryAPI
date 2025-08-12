using MarketInventory.Application.Interfaces;
using MarketInventory.Domain.Entities;

using MarketInventory.Application.Dtos;

namespace MarketInventory.Application.Services.Interfaces
{
    public interface IKullaniciTuruService
    {
        Task<IEnumerable<KullaniciTuruDto>> GetAllAsync();
        Task<KullaniciTuruDto?> GetByIdAsync(int id);
        Task AddAsync(CreateKullaniciTuruDto dto);
        Task UpdateAsync(int id, CreateKullaniciTuruDto dto);
        Task DeleteAsync(int id);
    }
}

