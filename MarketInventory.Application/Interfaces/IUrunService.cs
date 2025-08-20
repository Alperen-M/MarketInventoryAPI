using MarketInventory.Application.Interfaces;
using MarketInventory.Domain.Entities;
using MarketInventory.Application.DTOs;

namespace MarketInventory.Application.Interfaces
{
    public interface IUrunService
    {
        Task<IEnumerable<UrunReadDto>> GetAllAsync();
        Task<UrunReadDto?> GetByIdAsync(int id);
        Task<UrunReadDto> AddAsync(UrunCreateDto dto);
        Task<bool> UpdateAsync(int id, UrunUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}