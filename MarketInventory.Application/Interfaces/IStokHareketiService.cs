using MarketInventory.Application.Interfaces;
using MarketInventory.Domain.Entities;
using MarketInventory.Application.DTOs;

namespace MarketInventory.Application.Interfaces
{
    public interface IStokHareketiService
    {
        Task<IEnumerable<StokHareketiReadDto>> GetAllAsync();
        Task<StokHareketiReadDto?> GetByIdAsync(int id);
        Task<StokHareketiReadDto> AddAsync(StokHareketiCreateDto dto);
        Task<bool> UpdateAsync(int id, StokHareketiUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}